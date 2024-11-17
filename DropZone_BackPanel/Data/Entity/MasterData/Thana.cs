using System.ComponentModel.DataAnnotations;

namespace DropZone_BackPanel.Data.Entity.MasterData
{
    public class Thana : Base
    {
       
        public string? thanaCode { get; set; }
      
        public string? thanaName { get; set; }

        public string? shortName { get; set; }
        public string? thanaNameBn { get; set; }

        public int districtId { get; set; }

        public District? district { get; set; }
    }
}
