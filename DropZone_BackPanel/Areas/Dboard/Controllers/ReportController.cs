using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using DropZone_BackPanel.ERPServices.ReportData.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class ReportController : Controller
    {
        private readonly IPersonData _persondata;
        private readonly ISettingsService settingsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportController(IPersonData persondata, ISettingsService settingsService, UserManager<ApplicationUser> userManager)
        {
            _persondata = persondata;
            this.settingsService = settingsService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                var model = new ReportViewModel
                {
                    rangeMetros = await settingsService.GetAllRangeMetro(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDivisionDistrictByRangeId(int Id)
        {
            return Json(await settingsService.GetDivisionDistrictByRangeId(Id));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetZoneCircleByDivisionId(int Id)
        {
            return Json(await settingsService.GetZoneCircleByDivisionId(Id));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPoliceThanaByZoneid(int Id)
        {
            return Json(await settingsService.GetPoliceThanaByZoneid(Id));
        }

        [Route("global/api/GetReportData/{range}/{district}/{zone}/{thana}")]
        public async Task<IActionResult> GetReportData(int range,int district,int zone,int thana)
        {
            var result=await _persondata.GetPersonDataWithFilesByFiltersAsync(range,district,zone,thana);
            return Json(result);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OtpLogList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Admin"))
            {
                var model = new OtpLogsViewModel
                {
                    otpLogs = await settingsService.GetAllOtplogsList(),
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account", new { Area = "Auth" });
            }
        }

    }
}
