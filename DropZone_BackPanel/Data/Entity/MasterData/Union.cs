using System.ComponentModel.DataAnnotations;

namespace DropZone_BackPanel.Data.Entity.MasterData
{
    public class Union : Base
    {
        public int? thanaId { get; set; }
        public Thana? thana { get; set; }
        [MaxLength(100)]
        public string? unionNameBn { get; set; }
        public string? unionNameEn { get; set; }

        
    }
}
