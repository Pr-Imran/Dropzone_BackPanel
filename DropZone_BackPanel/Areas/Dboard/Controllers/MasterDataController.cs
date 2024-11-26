﻿using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Areas.Dboard.Models;
using DropZone_BackPanel.Areas.Dboard.Models.Lang;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.Helpers;
using DropZone_BackPanel.Services.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;

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
    }
}
