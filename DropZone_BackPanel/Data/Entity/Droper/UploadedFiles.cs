﻿namespace DropZone_BackPanel.Data.Entity.Droper
{
    public class UploadedFiles:Base
    {
        public int? personsDataId { get; set; }
        public PersonsData? personsData { get; set; }
        public string? attachmentUrl { get; set; }
    }
}
