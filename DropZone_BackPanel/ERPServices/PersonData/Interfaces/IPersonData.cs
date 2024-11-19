using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Data.Entity.Droper;

namespace DropZone_BackPanel.ERPServices.PersonData.Interfaces
{
    public interface IPersonData
    {
        Task<int> AddPersonsDataAsync(PersonsData personsData);
        Task AddUploadedFilesAsync(IEnumerable<UploadedFiles> uploadedFiles);
        Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByMobileAsync(string mobile);
        Task<Dictionary<int, int>> GetHourlyDataCountAsync(DateTime date);
    }
}
