using DropZone_BackPanel.API.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData.PoliceMapping;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.ERPServices.PersonData.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    //Name = p.name,
                    Mobile = p.mobile,
                    UnionId = p.unionId,
                    UnionName = p.union != null ? p.union.unionName : null,
                    VillageId = p.villageId,
                    VillageName = p.village != null ? p.village.villageName : null,
                    Latitude = p.latitude,
                    Longitude = p.longitude,
                    createdAt = p.createdAt,
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
                            AttachmentUrl = uf.attachmentUrl,
                            uploadDatetime = uf.createdAt
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

        #region comment
        //public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesAsync(DateTime date, int? hour = null)
        //{
        //    var startDate = date.Date;
        //    var endDate = startDate.AddDays(1);

        //    IQueryable<PersonsData> query = _context.Set<PersonsData>().AsQueryable();

        //    if (hour.HasValue)
        //    {
        //        var hourStart = startDate.AddHours(hour.Value);
        //        var hourEnd = hourStart.AddHours(1);
        //        query = query.Where(p => p.createdAt >= hourStart && p.createdAt < hourEnd);
        //    }
        //    else
        //    {
        //        query = query.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
        //    }

        //    var personData = await query
        //        .Include(p => p.union)
        //        .Include(p => p.village)
        //        .ToListAsync();

        //    var personIds = personData.Select(p => p.Id).ToList();

        //    var uploadedFiles = await _context.Set<UploadedFiles>().Include(x=>x.crimeType)
        //        .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
        //        .ToListAsync();

        //    var data = personData.Select(p => new PersonDataWithFilesDto
        //    {
        //        Id = p.Id,
        //        Name = p.name,
        //        Mobile = p.mobile,
        //        UnionId = p.unionId,
        //        UnionName = p.union?.unionName,
        //        VillageId = p.villageId,
        //        VillageName = p.village?.villageName,
        //        Latitude = p.latitude,
        //        Longitude = p.longitude,
        //        UploadedFiles = uploadedFiles
        //            .Where(uf => uf.personsDataId == p.Id)
        //            .Select(uf => new UploadedFileDto
        //            {
        //                Id = uf.Id,
        //                AttachmentUrl = uf.attachmentUrl
        //            }).ToList(),
        //        crimeType = uploadedFiles
        //                    .Where(uf => uf.personsDataId == p.Id)
        //                    .Select(uf => uf.crimeType?.crimeType)
        //                    .FirstOrDefault()
        //    }).ToList();
        //    return data;
        //}


        //public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByPoliceThanaIdAsync(int policethanaId)
        //{
        //    // Step 1: Get the Thana ID associated with the Police Thana
        //    var thanaId = await _context.Set<PoliceThana>()
        //        .Where(pt => pt.Id == policethanaId)
        //        .Select(pt => pt.upazillaId)
        //        .FirstOrDefaultAsync();

        //    if (thanaId == null)
        //    {
        //        return new List<PersonDataWithFilesDto>(); // Return empty if no Thana found
        //    }

        //    // Step 2: Get Union IDs related to the Thana ID
        //    var unionIds = await _context.Set<UnionWard>()
        //        .Where(u => u.thanaId == thanaId)
        //        .Select(u => u.Id)
        //        .ToListAsync();

        //    // Step 3: Get Village IDs related to the Union IDs
        //    var villageIds = await _context.Set<Village>()
        //        .Where(v => unionIds.Contains(v.unionWardId))
        //        .Select(v => v.Id)
        //        .ToListAsync();

        //    // Step 4: Get PersonsData filtered by Union and Village IDs
        //    var personData = await _context.Set<PersonsData>()
        //        .Where(p => unionIds.Contains(p.unionId ?? 0) || villageIds.Contains(p.villageId ?? 0))
        //        .Include(p => p.union)
        //        .Include(p => p.village)
        //        .ToListAsync();

        //    // Step 5: Get Uploaded Files for the filtered PersonsData
        //    var personIds = personData.Select(p => p.Id).ToList();
        //    var uploadedFiles = await _context.Set<UploadedFiles>().Include(x=>x.crimeType)
        //        .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
        //        .ToListAsync();

        //    // Step 6: Map data to PersonDataWithFilesDto
        //    var data = personData.Select(p => new PersonDataWithFilesDto
        //    {
        //        Id = p.Id,
        //        Name = p.name,
        //        Mobile = p.mobile,
        //        UnionId = p.unionId,
        //        UnionName = p.union?.unionName,
        //        VillageId = p.villageId,
        //        VillageName = p.village?.villageName,
        //        Latitude = p.latitude,
        //        Longitude = p.longitude,
        //        UploadedFiles = uploadedFiles
        //                         .Where(uf => uf.personsDataId == p.Id)
        //                         .Select(uf => new UploadedFileDto
        //                         {
        //                             Id = uf.Id,
        //                             AttachmentUrl = uf.attachmentUrl,
        //                         }).ToList(),
        //        crimeType = uploadedFiles
        //                    .Where(uf => uf.personsDataId == p.Id)
        //                    .Select(uf => uf.crimeType?.crimeType)
        //                    .FirstOrDefault()
        //    }).ToList();

        //    return data;
        //}
        #endregion


        //public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesAsync(DateTime date, int? hour = null)
        //{
        //    var startDate = date.Date;
        //    var endDate = startDate.AddDays(1);

        //    IQueryable<PersonsData> query = _context.Set<PersonsData>().AsQueryable();

        //    if (hour.HasValue)
        //    {
        //        var hourStart = startDate.AddHours(hour.Value);
        //        var hourEnd = hourStart.AddHours(1);
        //        query = query.Where(p => p.createdAt >= hourStart && p.createdAt < hourEnd);
        //    }
        //    else
        //    {
        //        query = query.Where(p => p.createdAt >= startDate && p.createdAt < endDate);
        //    }

        //    var personData = await query
        //        .Include(p => p.union)
        //        .Include(p => p.village)
        //        .ToListAsync();

        //    var personIds = personData.Select(p => p.Id).ToList();

        //    var uploadedFiles = await _context.Set<UploadedFiles>().Include(x => x.crimeType)
        //        .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
        //        .ToListAsync();

        //    // Step 1: Get Upazilla IDs (thanaIds) from PersonsData
        //    var upazillaIds = personData
        //        .Select(p => p.union?.thanaId ?? 0) // Assuming union has a `thanaId` field
        //        .Distinct()
        //        .Where(id => id > 0)
        //        .ToList();

        //    // Step 2: Retrieve range, district, zone, and thana names
        //    var policeThanas = await _context.Set<PoliceThana>()
        //        .Where(pt => upazillaIds.Contains((int)pt.upazillaId))
        //        .ToListAsync();

        //    var rangeName = policeThanas
        //        .Select(pt => pt.rangeMetroId)
        //        .Distinct()
        //        .Select(rangeId => _context.Set<RangeMetro>()
        //            .Where(r => r.Id == rangeId)
        //            .Select(r => r.rangeMetroNameBn)
        //            .FirstOrDefault())
        //        .FirstOrDefault();

        //    var districtName = policeThanas
        //        .Select(pt => pt.divisionDistrictId)
        //        .Distinct()
        //        .Select(districtId => _context.Set<DivisionDistrict>()
        //            .Where(d => d.Id == districtId)
        //            .Select(d => d.divisionDistrictNameBn)
        //            .FirstOrDefault())
        //        .FirstOrDefault();

        //    var zoneName = policeThanas
        //        .Select(pt => pt.zoneCircleId)
        //        .Distinct()
        //        .Select(zoneId => _context.Set<ZoneCircle>()
        //            .Where(z => z.Id == zoneId)
        //            .Select(z => z.zoneNameBn)
        //            .FirstOrDefault())
        //        .FirstOrDefault();

        //    var thanaName = policeThanas
        //        .Select(pt => pt.policeThanaNameBn)
        //        .FirstOrDefault();

        //    // Step 3: Map data and include districtDetails
        //    var data = personData.Select(p => new PersonDataWithFilesDto
        //    {
        //        Id = p.Id,
        //        Name = p.name,
        //        Mobile = p.mobile,
        //        UnionId = p.unionId,
        //        UnionName = p.union?.unionName,
        //        VillageId = p.villageId,
        //        VillageName = p.village?.villageName,
        //        Latitude = p.latitude,
        //        Longitude = p.longitude,
        //        UploadedFiles = uploadedFiles
        //            .Where(uf => uf.personsDataId == p.Id)
        //            .Select(uf => new UploadedFileDto
        //            {
        //                Id = uf.Id,
        //                AttachmentUrl = uf.attachmentUrl,
        //                uploadDatetime = uf.createdAt
        //            }).ToList(),
        //        crimeType = uploadedFiles
        //            .Where(uf => uf.personsDataId == p.Id)
        //            .Select(uf => uf.crimeType?.crimeType)
        //            .FirstOrDefault(),
        //        districtDetails = string.Join(" , ", new[] { rangeName, districtName, zoneName, thanaName }
        //            .Where(name => !string.IsNullOrEmpty(name))) // Concatenate non-empty names
        //    }).ToList();

        //    return data;
        //}
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

            var uploadedFiles = await _context.Set<UploadedFiles>().Include(x => x.crimeType)
                .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
                .ToListAsync();

            // Step 1: Get all upazillaIds (thanaIds) from PersonsData
            var upazillaIds = personData
                .Select(p => p.union?.thanaId ?? 0) // Assuming union has a `thanaId` field
                .Distinct()
                .Where(id => id > 0)
                .ToList();

            // Step 2: Retrieve all PoliceThana data for the collected upazillaIds
            var policeThanas = await _context.Set<PoliceThana>()
                .Where(pt => upazillaIds.Contains((int)pt.upazillaId))
                .ToListAsync();

            // Step 3: Map data for each person's district details
            var data = personData.Select(p => {
                // Get upazillaId for the current person's union
                var upazillaId = p.union?.thanaId;

                // Filter PoliceThana data specific to the current upazillaId
                var currentPoliceThanas = policeThanas
                    .Where(pt => pt.upazillaId == upazillaId)
                    .ToList();

                // Compute rangeName, districtName, zoneName, and thanaName for the current person
                var currentRangeName = currentPoliceThanas
                    .Select(pt => pt.rangeMetroId)
                    .Distinct()
                    .Select(rangeId => _context.Set<RangeMetro>()
                        .Where(r => r.Id == rangeId)
                        .Select(r => r.rangeMetroNameBn)
                        .FirstOrDefault())
                    .FirstOrDefault();

                var currentDistrictName = currentPoliceThanas
                    .Select(pt => pt.divisionDistrictId)
                    .Distinct()
                    .Select(districtId => _context.Set<DivisionDistrict>()
                        .Where(d => d.Id == districtId)
                        .Select(d => d.divisionDistrictNameBn)
                        .FirstOrDefault())
                    .FirstOrDefault();

                var currentZoneName = currentPoliceThanas
                    .Select(pt => pt.zoneCircleId)
                    .Distinct()
                    .Select(zoneId => _context.Set<ZoneCircle>()
                        .Where(z => z.Id == zoneId)
                        .Select(z => z.zoneNameBn)
                        .FirstOrDefault())
                    .FirstOrDefault();

                var currentThanaName = currentPoliceThanas
                    .Select(pt => pt.policeThanaNameBn)
                    .FirstOrDefault();

                return new PersonDataWithFilesDto
                {
                    Id = p.Id,
                    //Name = p.name,
                    Mobile = p.mobile,
                    UnionId = p.unionId,
                    UnionName = p.union?.unionName,
                    VillageId = p.villageId,
                    VillageName = p.village?.villageName,
                    Latitude = p.latitude,
                    Longitude = p.longitude,
                    createdAt = uploadedFiles
                    .Where(uf => uf.personsDataId == p.Id)
                    .Select(uf => uf.createdAt)
                    .FirstOrDefault(),
                    UploadedFiles = uploadedFiles
                        .Where(uf => uf.personsDataId == p.Id)
                        .Select(uf => new UploadedFileDto
                        {
                            Id = uf.Id,
                            AttachmentUrl = uf.attachmentUrl,
                            uploadDatetime = uf.createdAt
                        }).ToList(),
                    crimeType = uploadedFiles
                        .Where(uf => uf.personsDataId == p.Id)
                        .Select(uf => uf.crimeType?.crimeType)
                        .FirstOrDefault(),
                    districtDetails = string.Join(" , ", new[] { currentRangeName, currentDistrictName, currentZoneName, currentThanaName }
                        .Where(name => !string.IsNullOrEmpty(name))) // Concatenate non-empty names
                };
            }).ToList();

            return data;
        }


        public async Task<List<PersonDataWithFilesDto>> GetPersonDataWithFilesByFiltersAsync(int? rangeId, int? districtId, int? zoneId, int? policethanaId)
        {
            // Step 1: Initialize filters
            IQueryable<PoliceThana> policeThanaQuery = _context.Set<PoliceThana>();

            if (rangeId.HasValue && rangeId>0)
            {
                policeThanaQuery = policeThanaQuery.Where(pt => pt.rangeMetroId == rangeId.Value);
            }

            if (districtId.HasValue && districtId>0)
            {
                policeThanaQuery = policeThanaQuery.Where(pt => pt.divisionDistrictId == districtId.Value);
            }

            if (zoneId.HasValue && zoneId > 0)
            {
                policeThanaQuery = policeThanaQuery.Where(pt => pt.zoneCircleId == zoneId.Value);
            }

            if (policethanaId.HasValue && policethanaId > 0)
            {
                policeThanaQuery = policeThanaQuery.Where(pt => pt.Id == policethanaId.Value);
            }

            // Step 2: Get Thana IDs
            var thanaIds = await policeThanaQuery.Select(pt => pt.upazillaId).ToListAsync();

            if (!thanaIds.Any())
            {
                return new List<PersonDataWithFilesDto>(); // Return empty if no data matches the filter
            }

            // Step 3: Get Union and Village IDs
            var unionIds = await _context.Set<UnionWard>()
                .Where(u => thanaIds.Contains(u.thanaId))
                .Select(u => u.Id)
                .ToListAsync();

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
                .Include(x => x.crimeType)
                .Where(uf => personIds.Contains(uf.personsDataId ?? 0))
                .ToListAsync();

            // Step 6: Map data to PersonDataWithFilesDto
            var data = personData.Select(p => new PersonDataWithFilesDto
            {
                Id = p.Id,
                //Name = p.name,
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
                    }).ToList(),
                crimeType = uploadedFiles
                    .Where(uf => uf.personsDataId == p.Id)
                    .Select(uf => uf.crimeType?.crimeType)
                    .FirstOrDefault(),
                createdAt = uploadedFiles
                    .Where(uf => uf.personsDataId == p.Id)
                    .Select(uf => uf.createdAt)
                    .FirstOrDefault(),
            }).ToList();

            // Step 7: Concatenate Names
            var rangeName = rangeId.HasValue
                ? await _context.Set<RangeMetro>()
                    .Where(r => r.Id == rangeId.Value)
                    .Select(r => r.rangeMetroNameBn)
                    .FirstOrDefaultAsync()
                : null;

            var districtName = districtId.HasValue
                ? await _context.Set<DivisionDistrict>()
                    .Where(d => d.Id == districtId.Value)
                    .Select(d => d.divisionDistrictNameBn)
                    .FirstOrDefaultAsync()
                : null;

            var zoneName = zoneId.HasValue
                ? await _context.Set<ZoneCircle>()
                    .Where(z => z.Id == zoneId.Value)
                    .Select(z => z.zoneNameBn)
                    .FirstOrDefaultAsync()
                : null;

            var thanaName = policethanaId.HasValue
                ? await _context.Set<PoliceThana>()
                    .Where(t => t.Id == policethanaId.Value)
                    .Select(t => t.policeThanaNameBn)
                    .FirstOrDefaultAsync()
                : null;

            // Optionally add concatenated names to the result DTO
            foreach (var item in data)
            {
                item.districtDetails = string.Join(" , ", new[] { rangeName, districtName, zoneName, thanaName }
                    .Where(name => !string.IsNullOrEmpty(name))); // Concatenate non-empty names
            }

            return data;
        }

    }
}
