using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Areas.Dboard.Model;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.AuthService;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class DashBoardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoes userInfoes;
        private readonly IPersonData _persondata;
        public DashBoardController(IUserInfoes userInfoes, IConfiguration configuration, IPersonData persondata)
        {
            this.userInfoes = userInfoes;
            _configuration = configuration;
            _persondata = persondata;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
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


        public async Task<IActionResult> UserUploadDetailsDetails(int? hour, DateTime date)
        {
            var data = await _persondata.GetPersonDataWithFilesAsync(date, hour);
            var viewModel = data.Select(p => new UserUploadDetailsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Mobile = p.Mobile,
                UnionName = p.UnionName,
                VillageName = p.VillageName,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                UploadedFiles = p.UploadedFiles.Select(uf => new UploadedFileDto
                {
                    Id = uf.Id,
                    AttachmentUrl = uf.AttachmentUrl
                }).ToList()
            }).ToList();
            ViewData["Date"] = date.ToString("yyyy-MM-dd"); 
            ViewData["Hour"] = hour.HasValue ? hour.Value.ToString("00") : "All hours"; 
            return View(viewModel);
        }

    }
}
