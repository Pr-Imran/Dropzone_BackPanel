using DropZone_BackPanel.Data.Entity.MasterData;

namespace DropZone_BackPanel.Data.Entity.Droper
{
    public class PersonsData:Base
    {
        public string? name { get; set; }
        public string? mobile { get; set; }
        public int? unionId { get; set; }
        public Union? union { get; set; }
        public int? villageId { get; set; }
        public Village? village { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
    }
}
