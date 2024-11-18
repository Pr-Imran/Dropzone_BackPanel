﻿using DropZone_BackPanel.Data.Entity.Droper;

namespace DropZone_BackPanel.ERPServices.PersonData.Interfaces
{
    public interface IPersonData
    {
        Task<int> AddPersonsDataAsync(PersonsData personsData);
        Task AddUploadedFilesAsync(IEnumerable<UploadedFiles> uploadedFiles);
    }
}