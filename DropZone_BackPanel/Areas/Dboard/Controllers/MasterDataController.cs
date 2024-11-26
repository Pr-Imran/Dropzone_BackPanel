using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.Areas.Dboard.Models;
using DropZone_BackPanel.Areas.Dboard.Models.Lang;
using DropZone_BackPanel.Data.Entity;
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


        public IActionResult Index()
        {
            return View();
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
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in role)
            {
                ApplicationRoleViewModel roleViewModel = new ApplicationRoleViewModel
                {
                    RoleName = data
                };
                lstRole.Add(roleViewModel);
            }
            VillageViewModel2 model0 = new VillageViewModel2
            {
                divisions = await _masterDataService.GetDivisionsByCountryId(1),
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
    }
}
