using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IUserInfoes userInfoes;
        private readonly IPersonData _persondata;
        private readonly IDbChangeService dbChangeService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploadController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserInfoes userInfoes, IDbChangeService dbChangeService, IPersonData persondata, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.userInfoes = userInfoes;
            this.dbChangeService = dbChangeService;
            _persondata = persondata;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadData( [FromForm] string? name, [FromForm] string? mobile, [FromForm] int? unionId, [FromForm] int? villageId, [FromForm] string? latitude, [FromForm] string? longitude, [FromForm] IFormFileCollection files)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(mobile) || files == null || files.Count == 0)
            {
                return BadRequest("Invalid input data.");
            }
            var personsData = new PersonsData
            {
                name = name,
                mobile = mobile,
                unionId = unionId,
                villageId = villageId,
                latitude = latitude,
                longitude = longitude
            };

            // Save PersonsData
            int personsDataId = await _persondata.AddPersonsDataAsync(personsData);

            // Validate and save files
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ufile");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            List<string> warningFiles = new List<string>();
            List<UploadedFiles> uploadedFilesList = new List<UploadedFiles>();

            foreach (var file in files)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".mp4")
                {
                    string filePath = Path.Combine(uploadFolder, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadedFilesList.Add(new UploadedFiles
                    {
                        personsDataId = personsDataId,
                        attachmentUrl = Path.Combine("ufile", file.FileName)
                    });
                }
                else
                {
                    warningFiles.Add(file.FileName);
                }
            }

            // Save UploadedFiles
            if (uploadedFilesList.Any())
            {
                await _persondata.AddUploadedFilesAsync(uploadedFilesList);
            }

            return Ok(new
            {
                Message = "Data uploaded successfully.",
                Warnings = warningFiles.Any() ? $"The following files were not saved: {string.Join(", ", warningFiles)}" : null
            });
        }




    }
}
