using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using System.ComponentModel;

namespace DropZone_BackPanel.Areas.Dboard.Model
{
    public class CrimeTypeViewModel2
    {
        public int Id { get; set; }
        public string? crimeType { get; set; }
        public bool? isActive { get; set; }

        public int? isDelete { get; set; }
        public DateTime? createdAt { get; set; }

        public IEnumerable<CrimeInfo>? crimeTypes { get; set; }
    }
}
