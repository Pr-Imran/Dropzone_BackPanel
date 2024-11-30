using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;

namespace DropZone_BackPanel.Areas.Dboard.Model
{
    public class FileLimitsViewModel
    {
        public IEnumerable<FileType>? fileTypes { get; set; }
        public IEnumerable<FileLimits>? fileLimites{ get; set; }

        public int Id { get; set; }
        public string? fileTypeName { get; set; }
        public bool isActive { get; set; }
    }
}
