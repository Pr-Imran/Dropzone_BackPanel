using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DropZone_BackPanel.ERPServices.PersonData
{
    public class PersonData: IPersonData
    {
        private readonly DropSpaceDbContext _context;

        public PersonData(DropSpaceDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPersonsDataAsync(PersonsData personsData)
        {
            _context.personalDatas.Add(personsData);
            await _context.SaveChangesAsync();
            return personsData.Id;
        }
        public async Task AddUploadedFilesAsync(IEnumerable<UploadedFiles> uploadedFiles)
        {
            _context.uploadedFiles.AddRange(uploadedFiles);
            await _context.SaveChangesAsync();
        }
    }
}
