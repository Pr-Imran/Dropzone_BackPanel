using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.AuthService;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using DropZone_BackPanel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class DashBoardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoes userInfoes;
        private readonly IPersonData _persondata;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashBoardController(IUserInfoes userInfoes, IConfiguration configuration, IPersonData persondata, UserManager<ApplicationUser> userManager)
        {
            this.userInfoes = userInfoes;
            _configuration = configuration;
            _persondata = persondata;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                var hourlyCounts = await _persondata.GetHourlyDataCountAsync(DateTime.Today);
                var dailyCounts = await _persondata.GetDailyDataCountAsync(DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));

                // Serialize daily counts with formatted date
                var formattedDailyCounts = dailyCounts.ToDictionary(
                    entry => entry.Key.ToString("yyyy-MM-dd"),  // Format date as string
                    entry => entry.Value
                );

                var fullHourlyCounts = new Dictionary<int, int>();
                for (int i = 0; i < 24; i++)
                {
                    fullHourlyCounts[i] = hourlyCounts.ContainsKey(i) ? hourlyCounts[i] : 0;
                }

                ViewBag.HourlyCounts = fullHourlyCounts;
                ViewBag.DailyCounts = formattedDailyCounts;

                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UserUploadDetailsDetails(int? hour, DateTime date)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                var data = await _persondata.GetPersonDataWithFilesAsync(date, hour);
                var viewModel = data.Select(p => new UserUploadDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Mobile = p.Mobile,
                    stringMobile=Helpers.IdMasking.Encode(p.Mobile),
                    UnionName = p.UnionName,
                    VillageName = p.VillageName,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    crimeType = p.crimeType,
                    districtDetails = p.districtDetails,
                    createdAt = p.createdAt,
                    crimeName = p.crimeName,
                    crimeDate = p.crimeDate,
                    crimePlace = p.crimePlace,
                    crimeTime = p.crimeTime,
                    UploadedFiles = p.UploadedFiles.Select(uf => new UploadedFileDto
                    {
                        Id = uf.Id,
                        AttachmentUrl = uf.AttachmentUrl,
                        shortUrl=uf.shortUrl,
                        stringShortUrl=IdMasking.Encode(uf.shortUrl),
                    }).ToList()
                }).ToList();
                ViewData["Date"] = date.ToString("yyyy-MM-dd");
                ViewData["Hour"] = hour.HasValue ? hour.Value.ToString("00") : "All hours";
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> StreamFile(string? shortUrl, string? userIdentifier)
        {
            string filePath = string.Empty;
            userIdentifier=IdMasking.Decode(userIdentifier);
            shortUrl = IdMasking.Decode(shortUrl);
            try
            {
                if (!string.IsNullOrEmpty(shortUrl) && !string.IsNullOrEmpty(userIdentifier))
                {
                    var fileData = await _persondata.GetUploadedFileByShortUrlAsync(shortUrl, userIdentifier);

                    //Console.WriteLine("Fetched file data: " + (fileData == null ? "null" : fileData.newFileName));

                    if (fileData == null || string.IsNullOrEmpty(fileData.newFileName))
                    {
                        return NotFound(new { Message = "File not found." });
                    }

                    string uploadFolder = Path.Combine("D:\\FileManagement", userIdentifier);
                    filePath = Path.Combine(uploadFolder, fileData.newFileName);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid request parameters." });
                }

                //Console.WriteLine("Requested file path: " + filePath);s
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { Message = "File not found on the server." });
                }

                string mimeType = Path.GetExtension(filePath).ToLower() switch
                {
                    ".mp4" => "video/mp4",
                    ".avi" => "video/x-msvideo",
                    ".mov" => "video/quicktime",
                    ".webm" => "video/webm",
                    _ => "application/octet-stream" // Default MIME type
                };

                //Console.WriteLine("MIME type: " + mimeType);

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                Response.ContentType = mimeType;
                Response.ContentLength = fileStream.Length;
                Response.Headers.Add("Content-Disposition", $"inline; filename={Path.GetFileName(filePath)}");

                var range = Request.Headers["Range"].ToString();

                if (!string.IsNullOrEmpty(range) && range.StartsWith("bytes="))
                {
                    var rangeParts = range.Replace("bytes=", "").Split('-');

                    //Console.WriteLine($"Range Header: {range}");
                   // Console.WriteLine($"Range Parts: {string.Join(",", rangeParts)}");

                    if (rangeParts.Length == 2 &&
                        long.TryParse(rangeParts[0], out long start) &&
                        long.TryParse(rangeParts[1], out long end))
                    {
                        Response.StatusCode = 206; 
                        Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{fileStream.Length}");
                        fileStream.Seek(start, SeekOrigin.Begin);

                        var buffer = new byte[8192]; 
                        var bytesRead = 0;
                        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await Response.Body.WriteAsync(buffer, 0, bytesRead);
                        }
                    }
                    else
                    {
                        await fileStream.CopyToAsync(Response.Body);
                    }
                }
                else
                {
                    await fileStream.CopyToAsync(Response.Body);
                }

                fileStream.Close();
            }
            catch (FormatException ex)
            {
                //Console.WriteLine("Format Exception: " + ex.Message);
                return BadRequest(new { Message = "Invalid format in range header or parameters.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error while streaming file: " + ex.Message);
                return StatusCode(500, new { Message = "Error streaming file.", Details = ex.Message });
            }

            return new EmptyResult();
        }





    }
}
