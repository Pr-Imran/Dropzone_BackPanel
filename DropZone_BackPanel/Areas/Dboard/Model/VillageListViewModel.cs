namespace DropZone_BackPanel.Areas.Dboard.Models
{
    public class VillageListViewModel
    {
        public int? Id { get; set; }
        public int? unionWardId { get; set; }
        public int? thanaId { get; set; }
        public int? districtId { get; set; }
        public int? divisionId { get; set; }

        public string? villageCode { get; set; }
        public string? villageName { get; set; }
        public string? villageNameBn { get; set; }
        public string? isActive { get; set; }
        public string? unionName { get; set; }
        public string? unionNameBn { get; set; }
        public string? thanaName { get; set; }
        public string? thanaNameBn { get; set; }
        public string? districtName { get; set; }
        public string? districtNameBn { get; set; }
        public string? divisionName { get; set; }
        public string? divisionNameBn { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
    }
}
