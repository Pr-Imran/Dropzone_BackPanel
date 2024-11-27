using DropZone_BackPanel.Data.Entity.LogInfo;
using DropZone_BackPanel.Data.Entity.MasterData.PoliceMapping;

namespace DropZone_BackPanel.ERPServices.ReportData.Interface
{
    public interface ISettingsService
    {
        Task<IEnumerable<RangeMetro>> GetAllRangeMetro();
        Task<IEnumerable<DivisionDistrict>> GetDivisionDistrictByRangeId(int rangeId);
        Task<IEnumerable<ZoneCircle>> GetZoneCircleByDivisionId(int divisionId);
        Task<IEnumerable<PoliceThana>> GetPoliceThanaByZoneid(int zoneId);
        Task<IEnumerable<OTPLogs>> GetAllOtplogsList();
    }
}
