using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.PersonData;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using DropZone_BackPanel.Helpers;
using DropZone_BackPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DropZone_BackPanel.Areas.Home.Controllers
{
    [Area("Auth")]
    public class HomeController : Controller
    {
        private readonly IPersonData _persondata;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMasterData _masterdata;

        public HomeController(IPersonData persondata, IMasterData masterData,  IWebHostEnvironment webHostEnvironment)
        {
            _persondata = persondata;
            _webHostEnvironment = webHostEnvironment;
            _masterdata = masterData;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetAllCountries()
        {
            var data= await _masterdata.GetAllCountries();
            return Json(data.Select(x => new CountryViewModel
            {
                countryNameBn = x.countryNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetDivisionByCountryId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetDivisionsByCountryId(Convert.ToInt32(IdMasking.Decode(model.cId)));
            return Json(data.Select(x => new DivisionViewModel
            {
                divisionNameBn = x.divisionNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetDistrictByDivisionId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetDistrictsByDivisonId(Convert.ToInt32(IdMasking.Decode(model.dId)));
            return Json(data.Select(x => new DistrictViewModel
            {
                districtNameBn = x.districtNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetThanaByDistrictId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetThanasByDistrictId(Convert.ToInt32(IdMasking.Decode(model.dId)));
            return Json(data.Select(x => new PoliceStationViewModel
            {
                policeThanaNameBn = x.thanaNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetActiveThanaByDistrictId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetActiveThanasByDistrictId(Convert.ToInt32(IdMasking.Decode(model.dId)));
            return Json(data.Select(x => new PoliceStationViewModel
            {
                policeThanaNameBn = x.thanaNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetUnionWardsByThanaId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetUnionWardsByThanaId(Convert.ToInt32(IdMasking.Decode(model.tId)));
            return Json(data.Select(x => new UnionWordViewModel
            {
                unionWordNameBn = x.unionNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetActiveUnionWardsByThanaId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetActiveUnionWardsByThanaId(Convert.ToInt32(IdMasking.Decode(model.tId)));
            return Json(data.Select(x => new UnionWordViewModel
            {
                unionWordNameBn = x.unionNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetAllVillageByUnionId([FromBody] MasterDataViewModel model)
        {
            var data = await _masterdata.GetAllVillageByUnionId(Convert.ToInt32(IdMasking.Decode(model.uId)));
            return Json(data.Select(x => new VillageViewModel
            {
                villageNameBn = x.villageNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[area]/[controller]/[action]")]
        public async Task<IActionResult> GetAllActiveVillageByUnionId([FromBody] MasterDataViewModel model)
        {
            var data= await _masterdata.GetAllActiveVillageByUnionId(Convert.ToInt32(IdMasking.Decode(model.uId)));
            return Json(data.Select(x => new VillageViewModel
            {
                villageNameBn = x.villageNameBn,
                Id = IdMasking.Encode(x.Id.ToString())
            }));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
