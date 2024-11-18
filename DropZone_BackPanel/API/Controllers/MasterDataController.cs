using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DropZone_BackPanel.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IUserInfoes userInfoes;
        private readonly IPersonData _persondata;
        private readonly IMasterData _masterdata;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MasterDataController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserInfoes userInfoes, IPersonData persondata, IWebHostEnvironment webHostEnvironment, IMasterData masterData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.userInfoes = userInfoes;
            _persondata = persondata;
            _webHostEnvironment = webHostEnvironment;
            _masterdata = masterData;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var countries = await _masterdata.GetAllCountries();
            return countries;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Division>> GetDivisionByCountryId(int id)
        {

            var districts = await _masterdata.GetDivisionsByCountryId(id);

            return districts;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<District>> GetDistrictByDivisionId(int id)
        {

            var districts = await _masterdata.GetDistrictsByDivisonId(id);

            return districts;
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<Thana>> GetThanaByDistrictId(int id)
        {
            var thanas = await _masterdata.GetThanasByDistrictId(id);

            return thanas;
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<Thana>> GetActiveThanaByDistrictId(int id)
        {
            var thanas = await _masterdata.GetActiveThanasByDistrictId(id);

            return thanas;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int id)
        {
            var unionWards = await _masterdata.GetUnionWardsByThanaId(id);

            return unionWards;
        }
        // GET: api/AddressMaster/GetActiveUnionWardsByThanaId
        [HttpGet("{id}")]
        public async Task<IEnumerable<UnionWard>> GetActiveUnionWardsByThanaId(int id)
        {
            var unionWards = await _masterdata.GetActiveUnionWardsByThanaId(id);

            return unionWards;
        }

        // GET: api/AddressMaster/GetAllVillageByUnionId
        [HttpGet("{id}")]
        public async Task<IEnumerable<Village>> GetAllVillageByUnionId(int id)
        {
            var villages = await _masterdata.GetAllVillageByUnionId(id);

            return villages;
        }
        // GET: api/AddressMaster/GetAllVillageByUnionId
        [HttpGet("{id}")]
        public async Task<IEnumerable<Village>> GetAllActiveVillageByUnionId(int id)
        {
            var villages = await _masterdata.GetAllActiveVillageByUnionId(id);

            return villages;
        }
    }
}
