using DropZone_BackPanel.Data;
using System.ComponentModel.DataAnnotations;

namespace DropZone_BackPanel.Data.Entity.MasterData
{
    public class Country : Base
    {
        [Required]
        public string? countryCode { get; set; }

        [Required]
        public string? countryName { get; set; }
        public string? countryNameBn { get; set; }

        
        public string? shortName { get; set; }

    }
}
