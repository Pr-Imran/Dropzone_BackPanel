using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByMobileAsync(string mobile)
        {
            var personDataList = await _context.personsDatas
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
                    UploadedFiles = new List<UploadedFileDto>()
                })
                .ToListAsync();

            if (personDataList.Any())
            {
                var personIds = personDataList.Select(p => p.Id).ToList();
                var uploadedFiles = await _context.uploadedFiles
                    .Where(uf => personIds.Contains((int)uf.personsDataId))
                    .Select(uf => new {
                        uf.personsDataId,
                        FileDto = new UploadedFileDto
                        {
                            Id = uf.Id,
                            AttachmentUrl = uf.attachmentUrl
                        }
                    })
                    .ToListAsync();
                foreach (var personData in personDataList)
                {
                    personData.UploadedFiles = uploadedFiles
                        .Where(uf => uf.personsDataId == personData.Id)
                        .Select(uf => uf.FileDto)
                        .ToList();
                }
            }

            return personDataList;
        }

        public async Task<Dictionary<int, int>> GetHourlyDataCountAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            // Query to group data by hour and count entries
            var hourlyData = await _context.Set<PersonsData>()
                .Where(p => p.createdAt >= startDate && p.createdAt < endDate)
                .GroupBy(p => p.createdAt.Value.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .ToListAsync();

            // Create a dictionary for hourly data counts
            var result = new Dictionary<int, int>();
            for (int i = 0; i < 24; i++)
            {
                result[i] = 0; // Initialize with zero
            }

            foreach (var data in hourlyData)
            {
                result[data.Hour] = data.Count;
            }

            return result;
        }

    }
}
