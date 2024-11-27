using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using DropZone_BackPanel.ERPServices.ReportData.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class ReportController : Controller
    {
        private readonly IPersonData _persondata;
        private readonly ISettingsService settingsService;

        public ReportController(IPersonData persondata, ISettingsService settingsService)
        {
            _persondata = persondata;
            this.settingsService = settingsService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new ReportViewModel
            {
                rangeMetros = await settingsService.GetAllRangeMetro(),
            };
            return View(model);
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

        [Route("global/api/GetReportData/{thana}")]
        public async Task<IActionResult> GetReportData(int thana)
        {
            var result=await _persondata.GetPersonDataWithFilesByPoliceThanaIdAsync(thana);
            return Json(result);
        }
    }
}
