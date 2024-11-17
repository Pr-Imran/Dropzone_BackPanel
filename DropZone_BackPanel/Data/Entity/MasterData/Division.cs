using System.ComponentModel.DataAnnotations;

namespace DropZone_BackPanel.Data.Entity.MasterData
{
    public class Division : Base
    {
        [Required]
        public string? divisionCode { get; set; }

        [Required]
        public string? divisionName { get; set; }
        public string? divisionNameBn { get; set; }

        public string? shortName { get; set; }

        public int countryId { get; set; }

        public Country? country { get; set; }
    }
}
