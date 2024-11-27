using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.LogInfo;
using DropZone_BackPanel.Data.Entity.MasterData.PoliceMapping;
using DropZone_BackPanel.ERPServices.ReportData.Interface;
using DropZone_BackPanel.Services.Dapper;
using Microsoft.EntityFrameworkCore;

namespace DropZone_BackPanel.ERPServices.ReportData
{
    public class SettingsService: ISettingsService
    {
        private readonly DropSpaceDbContext _context;
        private readonly IDapper _dapper;
        public SettingsService(DropSpaceDbContext context, IDapper dapper)
        {
            _context = context;
            _dapper = dapper;
        }
        public async Task<IEnumerable<RangeMetro>> GetAllRangeMetro()
        {
             var data = await _context.rangeMetros.ToListAsync();
             return data;
        }
        public async Task<IEnumerable<DivisionDistrict>> GetDivisionDistrictByRangeId(int rangeId)
        {
            var data = await _context.divisionDistricts.Where(X => X.rangeMetroId == rangeId).ToListAsync();
           
            return data;
        }
        public async Task<IEnumerable<ZoneCircle>> GetZoneCircleByDivisionId(int divisionId)
        {
            var data = await _context.zoneCircles.Where(X => X.divisionDistrictId == divisionId).ToListAsync();
            
            return data;
        }
        public async Task<IEnumerable<PoliceThana>> GetPoliceThanaByZoneid(int zoneId)
        {
            return await _context.policeThanas.Where(a => a.zoneCircleId == zoneId && a.isChild != 1).ToListAsync();
        }

        public async Task<IEnumerable<OTPLogs>> GetAllOtplogsList()
        {
            var data = await _context.oTPLogs.Where(x=>x.isVerified==false).ToListAsync();
            return data;
        }
    }
}
