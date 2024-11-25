using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;


namespace DropZone_BackPanel.Areas.Dboard.Models
{
    public class VillageViewModel2
    {
        public int villageId { get; set; }
        public string unionWardId { get; set; }
        public string? thanaId { get; set; }
        public string? divisionId { get; set; }
        public string? districtId { get; set; }
        public string villageCode { get; set; }
        public string villageName { get; set; }
        public string villageNameBn { get; set; }
        public string shortName { get; set; }
        public string isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<ApplicationRoleViewModel> applicationRoles { get; set; }
    }
}
