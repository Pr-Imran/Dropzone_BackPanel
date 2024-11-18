using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.Areas.Dboard.Controllers
{
    [Area("Dboard")]
    public class DashBoardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoes userInfoes;
        public DashBoardController(IUserInfoes userInfoes, IConfiguration configuration)
        {
            this.userInfoes = userInfoes;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var baseUrl = _configuration["Reports:URL"];
            string username = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
