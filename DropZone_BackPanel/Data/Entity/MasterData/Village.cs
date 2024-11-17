using System.ComponentModel.DataAnnotations;

namespace DropZone_BackPanel.Data.Entity.MasterData
{
    public class Village : Base
    {
        public int? unionId { get; set; }
        public Union? union { get; set; }
        [MaxLength(100)]
        public string? villageNameBn { get; set; }
        public string? villageNameEn { get; set; }

        
    }
}
