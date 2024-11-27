using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData.PoliceMapping;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByMobileAsync(string mobile)
        {
            var personDataList = await _context.personalDatas
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

            var hourlyData = await _context.Set<PersonsData>()
                .Where(p => p.createdAt >= startDate && p.createdAt < endDate)
                .GroupBy(p => p.createdAt.Value.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .ToListAsync();
            var result = new Dictionary<int, int>();
            for (int i = 0; i < 24; i++)
            {
                result[i] = 0; 
            }

            foreach (var data in hourlyData)
            {
                result[data.Hour] = data.Count;
            }

            return result;
        }

        public async Task<Dictionary<DateTime, int>> GetDailyDataCountAsync(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);

            var dailyData = await _context.Set<PersonsData>()
                .Where(p => p.createdAt >= startDate && p.createdAt < endDate)
                .GroupBy(p => p.createdAt.Value.Date) 
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();
            var result = new Dictionary<DateTime, int>();
            for (var date = startDate.Date; date < endDate; date = date.AddDays(1))
            {
                result[date] = 0;
            }
            foreach (var data in dailyData)
            {
                result[data.Date] = data.Count;
            }

            return result;
        }


        public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesAsync(DateTime date, int? hour = null)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            IQueryable<PersonsData> query = _context.Set<PersonsData>().AsQueryable();

            if (hour.HasValue)
            {
                var hourStart = startDate.AddHours(hour.Value);
                var hourEnd = hourStart.AddHours(1);
                query = query.Where(p => p.createdAt >= hourStart && p.createdAt < hourEnd);
            }
            else
            {
                query = query.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
            }

            var personData = await query
                .Include(p => p.union)
                .Include(p => p.village)
                .ToListAsync();

            var personIds = personData.Select(p => p.Id).ToList();

            var uploadedFiles = await _context.Set<UploadedFiles>()
                .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
                .ToListAsync();

            var data = personData.Select(p => new PersonDataWithFilesDto
            {
                Id = p.Id,
                Name = p.name,
                Mobile = p.mobile,
                UnionId = p.unionId,
                UnionName = p.union?.unionName,
                VillageId = p.villageId,
                VillageName = p.village?.villageName,
                Latitude = p.latitude,
                Longitude = p.longitude,
                UploadedFiles = uploadedFiles
                    .Where(uf => uf.personsDataId == p.Id)
                    .Select(uf => new UploadedFileDto
                    {
                        Id = uf.Id,
                        AttachmentUrl = uf.attachmentUrl
                    }).ToList()
            }).ToList();

            return data;
        }


        public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByPoliceThanaIdAsync(int policethanaId)
        {
            // Step 1: Get the Thana ID associated with the Police Thana
            var thanaId = await _context.Set<PoliceThana>()
                .Where(pt => pt.Id == policethanaId)
                .Select(pt => pt.upazillaId)
                .FirstOrDefaultAsync();

            if (thanaId == null)
            {
                return new List<PersonDataWithFilesDto>(); // Return empty if no Thana found
            }

            // Step 2: Get Union IDs related to the Thana ID
            var unionIds = await _context.Set<UnionWard>()
                .Where(u => u.thanaId == thanaId)
                .Select(u => u.Id)
                .ToListAsync();

            // Step 3: Get Village IDs related to the Union IDs
            var villageIds = await _context.Set<Village>()
                .Where(v => unionIds.Contains(v.unionWardId))
                .Select(v => v.Id)
                .ToListAsync();

            // Step 4: Get PersonsData filtered by Union and Village IDs
            var personData = await _context.Set<PersonsData>()
                .Where(p => unionIds.Contains(p.unionId ?? 0) || villageIds.Contains(p.villageId ?? 0))
                .Include(p => p.union)
                .Include(p => p.village)
                .ToListAsync();

            // Step 5: Get Uploaded Files for the filtered PersonsData
            var personIds = personData.Select(p => p.Id).ToList();
            var uploadedFiles = await _context.Set<UploadedFiles>()
                .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
                .ToListAsync();

            // Step 6: Map data to PersonDataWithFilesDto
            var data = personData.Select(p => new PersonDataWithFilesDto
            {
                Id = p.Id,
                Name = p.name,
                Mobile = p.mobile,
                UnionId = p.unionId,
                UnionName = p.union?.unionName,
                VillageId = p.villageId,
                VillageName = p.village?.villageName,
                Latitude = p.latitude,
                Longitude = p.longitude,
                UploadedFiles = uploadedFiles
                    .Where(uf => uf.personsDataId == p.Id)
                    .Select(uf => new UploadedFileDto
                    {
                        Id = uf.Id,
                        AttachmentUrl = uf.attachmentUrl,
                    })
                    .ToList()
            }).ToList();

            return data;
        }

    }
}
