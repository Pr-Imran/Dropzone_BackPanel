using DropZone_BackPanel.API.Models;

namespace DropZone_BackPanel.Areas.Dboard.Model
{
    public class UserUploadDetailsViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string UnionName { get; set; }
        public string VillageName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string crimeType { get; set; }
        public string districtDetails { get; set; }
        public DateTime? createdAt { get; set; }
        public List<UploadedFileDto> UploadedFiles { get; set; }
    }

    public class FileDetail
    {
        public int Id { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
