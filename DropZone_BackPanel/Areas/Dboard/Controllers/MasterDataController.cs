using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.Areas.Dboard.Models;
using DropZone_BackPanel.Areas.Dboard.Models.Lang;
using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using DropZone_BackPanel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class MasterDataController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMasterData _masterDataService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LangGenerate<SettingsLn> _lang;
        private readonly IConfiguration _configuration;
        

        public MasterDataController(IWebHostEnvironment _hostingEnvironment, IUserInfoes userInfoes, IMasterData masterDataService, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this._masterDataService = masterDataService;
            this._hostingEnvironment = _hostingEnvironment;
            _lang = new LangGenerate<SettingsLn>(_hostingEnvironment.ContentRootPath);
            _configuration = configuration;
        }
        #region Village
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Village()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                VillageViewModel2 model = new VillageViewModel2
                {
                    divisions = await _masterDataService.GetDivisionsByCountryId(1),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Village([FromForm] VillageViewModel2 model)
        {
            Village village = new Village
            {
                Id = model.villageId,
                unionWardId = Convert.ToInt32(IdMasking.Decode(model.unionWardId)),
                villageCode = model.villageCode,
                villageName = model.villageName,
                villageNameBn = model.villageNameBn,
                isActive = model.isActive,
                latitude = model.latitude,
                longitude = model.longitude
            };
            await _masterDataService.SaveVillage(village);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            VillageViewModel2 model0 = new VillageViewModel2
            {
                divisions = await _masterDataService.GetDivisionsByCountryId(1),
                villages=await _masterDataService.GetAllVillageByUnionId(Convert.ToInt32(IdMasking.Decode(model.unionWardId)))
            };
            return View(model0);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteVillageById(int Id)
        {
            await _masterDataService.DeleteVillageById(Id);
            return Json(true);
        }
        #endregion

        #region UnionWard
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnionWard()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                UnionWordViewModel2 model = new UnionWordViewModel2
                {
                    divisions = await _masterDataService.GetDivisionsByCountryId(1),
                    thanas = await _masterDataService.GetAllThanas(),
                    unionWards = await _masterDataService.GetAllUnionWards(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnionWard([FromForm] UnionWordViewModel2 model)
        {
            UnionWard unionWard = new UnionWard
            {
                Id = model.Id,
                thanaId = int.Parse(IdMasking.Decode(model.thanaId)),
                unionCode = model.unionCode,
                unionName = model.unionName,
                unionNameBn = model.unionNameBn,
                latitude = model.latitude,
                longitude = model.longitude,
                isActive = model.isActive,
                createdAt = DateTime.UtcNow,
            };

            await _masterDataService.SaveUnionWards(unionWard);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in role)
            {
                ApplicationRoleViewModel roleViewModel = new ApplicationRoleViewModel
                {
                    RoleName = data
                };
                lstRole.Add(roleViewModel);
            }
            //return View(model0);
            return RedirectToAction("UnionWard", "MasterData", new { Area = "Dboard" });


        }

        [HttpPost]
        public async Task<JsonResult> DeleteUnionWardById(string Id)
        {
            var decryptedId = IdMasking.Decode(Id);
            var isDelete = await _masterDataService.DeleteUnionWardById(int.Parse(decryptedId));
            return Json(isDelete);
        }
        #endregion

        #region Crime Type
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CrimeType()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                CrimeTypeViewModel2 model = new CrimeTypeViewModel2
                {
                    crimeTypes = await _masterDataService.GetAllCrimeTypes(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrimeType([FromForm] CrimeTypeViewModel2 model)
        {
            CrimeInfo crimeInfo = new CrimeInfo
            {
                Id = model.Id,
                crimeType = model.crimeType,
                isActive = model.isActive,
                createdAt = DateTime.UtcNow,    
            };
            await _masterDataService.SaveCrimeType(crimeInfo);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in role)
            {
                ApplicationRoleViewModel roleViewModel = new ApplicationRoleViewModel
                {
                    RoleName = data
                };
                lstRole.Add(roleViewModel);
            }
            CrimeTypeViewModel2 model0 = new CrimeTypeViewModel2
            {
                crimeTypes = await _masterDataService.GetAllCrimeTypes(),
            };
            //return View(model0);
            return RedirectToAction("CrimeType", "MasterData", new { Area = "Dboard" });


        }

        [HttpPost]
        public async Task<JsonResult> DeleteCrimeTypeById(string Id)
        {
           var decryptedId = IdMasking.Decode(Id);
           var isDelete = await _masterDataService.DeleteCrimeTypeById(int.Parse(decryptedId));
           return Json(isDelete);
        }
        #endregion


        #region file limit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FileLimits()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                FileLimitsViewModel model = new FileLimitsViewModel
                {
                    fileTypes = await _masterDataService.GetAllFileTypes(),
                    fileLimites = await _masterDataService.GetAllFileLimits(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }
        [HttpPost]
        public IActionResult SaveFileLimits(FileLimits fileLimits)
        {
            try
            {
                // If the new record is set to active, deactivate existing records
                if (fileLimits.isActive)
                {
                    _masterDataService.DeactivateAllFileLimits();
                }

                // Check if it's an edit or a new entry
                if (fileLimits.Id > 0)
                {
                    var existingLimit = _masterDataService.GetFileLimitsById(fileLimits.Id);
                    if (existingLimit != null)
                    {
                        existingLimit.fileTypeId = fileLimits.fileTypeId;
                        existingLimit.hourFileNo = fileLimits.hourFileNo;
                        existingLimit.hourFileSize = fileLimits.hourFileSize;
                        existingLimit.dayFileNo = fileLimits.dayFileNo;
                        existingLimit.dayFileSize = fileLimits.dayFileSize;
                        existingLimit.totalFileNo = fileLimits.totalFileNo;
                        existingLimit.totalFileSize = fileLimits.totalFileSize;
                        existingLimit.alertFileSize = fileLimits.alertFileSize;
                        existingLimit.archiveFileSize = fileLimits.archiveFileSize;
                        existingLimit.archivingMonth = fileLimits.archivingMonth;
                        existingLimit.isActive = fileLimits.isActive;

                        _masterDataService.UpdateFileLimits(existingLimit);
                    }
                }
                else
                {
                    // Add a new record
                    _masterDataService.AddFileLimits(fileLimits);
                }

                // Save changes to the database
                _masterDataService.SaveFileLimits();

                return Json(new { success = true, message = "File limits saved successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while saving file limits." });
            }
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FileTypes()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                FileLimitsViewModel model = new FileLimitsViewModel
                {
                    fileTypes = await _masterDataService.GetAllFileTypes(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> FileTypes([FromForm] FileLimitsViewModel model)
        {
            FileType fileInfo = new FileType
            {
                Id = model.Id,
                fileTypeName = model.fileTypeName,
                isActive = model.isActive,
                createdAt = DateTime.UtcNow,
            };
            await _masterDataService.SaveFileType(fileInfo);
            return RedirectToAction("FileTypes", "MasterData", new { Area = "Dboard" });


        }
        #endregion
    }
}
