using DropSpace.Data.Entity.Droper;
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
        Task<Dictionary<DateTime, int>> GetDailyDataCountAsync(DateTime startDate, DateTime endDate);
        Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesAsync(DateTime date, int? hour = null);
        //Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByPoliceThanaIdAsync(int policethanaId);
        Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByFiltersAsync(int? rangeId, int? districtId, int? zoneId, int? policethanaId);
        Task<UploadedFileDto?> GetUploadedFileByShortUrlAsync(string shortUrl, string userIdentifier);
    }
}
