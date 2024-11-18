using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DropZone_BackPanel.ERPServices.PersonData
{
    public class PersonData: IPersonData
    {
        private readonly DropZoneDbContext _context;

        public PersonData(DropZoneDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPersonsDataAsync(PersonsData personsData)
        {
            _context.personsDatas.Add(personsData);
            await _context.SaveChangesAsync();
            return personsData.Id;
        }
        public async Task AddUploadedFilesAsync(IEnumerable<UploadedFiles> uploadedFiles)
        {
            _context.uploadedFiles.AddRange(uploadedFiles);
            await _context.SaveChangesAsync();
        }
        public async Task<PersonDataWithFilesDto?> GetPersonDataWithFilesByMobileAsync(string mobile)
        {
            var personData = await _context.personsDatas
                .Where(p => p.mobile == mobile)
                .Select(p => new PersonDataWithFilesDto
                {
                    Id = p.Id,
                    Name = p.name,
                    Mobile = p.mobile,
                    UnionId = p.unionId,
                    UnionName = p.union != null ? p.union.unionName : null,
                    VillageId = p.villageId,
                    VillageName = p.village != null ? p.village.villageName : null,
                    Latitude = p.latitude,
                    Longitude = p.longitude,
                })
                .FirstOrDefaultAsync();

            if (personData != null)
            {
                // Fetch related UploadedFiles
                personData.UploadedFiles = await _context.uploadedFiles
                    .Where(uf => uf.personsDataId == personData.Id)
                    .Select(uf => new UploadedFileDto
                    {
                        Id = uf.Id,
                        AttachmentUrl = uf.attachmentUrl
                    })
                    .ToListAsync();
            }

            return personData;
        }



    }
}
