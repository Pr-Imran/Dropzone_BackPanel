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
            var baseUrl = _configuration["Reports:URL"];
            string username = HttpContext.User.Identity.Name;
            //var hourlyCounts = await _persondata.GetHourlyDataCountAsync(DateTime.Today);
            var date=DateTime.Today;
            var hourlyCounts1 = await _persondata.GetHourlyDataCountAsync(date.AddDays(-1));


            return View();
        }
    }
}
