using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropZone_BackPanel.ERPServices.MasterData
{
    public class MasterDataService : IMasterData
    {
        private readonly DropSpaceDbContext _context;

        public MasterDataService(DropSpaceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var data = await _context.countries.ToListAsync();
            return data;
        }
        public async Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            return await _context.divisions.Where(X => X.countryId == CntId).ToListAsync();
        }
        public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        {
            return await _context.districts.Where(X => X.divisionId == DivisionId).ToListAsync();
        }
        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId)
        {
            return await _context.thanas.Where(x => x.districtId == DistrictId).ToListAsync();
        }
        public async Task<IEnumerable<Thana>> GetActiveThanasByDistrictId(int DistrictId)
        {
            return await _context.thanas.Where(x => x.districtId == DistrictId && x.isActive != "Inactive" && x.thanaNameBn != ".").ToListAsync();
        }
        public async Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId)
        {

            var data = await _context.unionWards.Where(X => X.thanaId == thanaId).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<UnionWard>> GetActiveUnionWardsByThanaId(int thanaId)
        {

            var data = await _context.unionWards.Include(a => a.thana.district.division).Where(X => X.thanaId == thanaId && X.isActive != "Inactive").ToListAsync();
            return data;
        }
        public async Task<int> SaveVillage(Village village)
        {
            if (village.Id != 0)
            {
                _context.villages.Update(village);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                await _context.villages.AddAsync(village);
                await _context.SaveChangesAsync();
                return 1;
            }
        }
        public async Task<IEnumerable<Village>> GetAllVillageByUnionId(int id)
        {
            return await _context.villages.Where(x => x.unionWardId == id).Include(x=>x.unionWard).ThenInclude(x=>x.thana).ThenInclude(x=>x.district).ThenInclude(x=>x.division).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<IEnumerable<Village>> GetAllActiveVillageByUnionId(int id)
        {
            return await _context.villages.Where(x => x.unionWardId == id && x.isActive != "Inactive").OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<bool> DeleteVillageById(int id)
        {
            _context.villages.Remove(await _context.villages.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #region Crime Types
        public async Task<IEnumerable<CrimeInfo>> GetAllCrimeTypes()
        {
            var data = await _context.crimeInfos.ToListAsync();
            var transformedData = data.Select(c =>
            {
                c.isActive = c.isActive.HasValue && c.isActive.Value ? true : false;
                return c;
            });
            return transformedData;
        }

        public async Task<int> SaveCrimeType(CrimeInfo crimeInfo)
        {
            if (crimeInfo.Id != 0)
            {
                _context.crimeInfos.Update(crimeInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                await _context.crimeInfos.AddAsync(crimeInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
        }


        public async Task<bool> DeleteCrimeTypeById(int id)
        {
            var status = true;
            var data = await _context.crimeInfos.FindAsync(id);
            try
            {
                if (data != null)
                {
                    _context.crimeInfos.Remove(data);
                    await _context.SaveChangesAsync();
                }
                return status;
            }
            catch (Exception)
            {

                return status = false;
            }

        }
        #endregion

        #region UnionWard
        public async Task<IEnumerable<UnionWard>> GetAllUnionWards()
        {
            var data = await _context.unionWards.OrderByDescending(u => u.Id).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Thana>> GetAllThanas()
        {
            var data = await _context.thanas.ToListAsync();
            return data;
        }

        public async Task<int> SaveUnionWards(UnionWard unionWard)
        {
            if (unionWard.Id != 0)
            {
                _context.unionWards.Update(unionWard);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                await _context.unionWards.AddAsync(unionWard);
                await _context.SaveChangesAsync();
                return 1;
            }
        }


        public async Task<bool> DeleteUnionWardById(int id)
        {
            var status = true;
            var data = await _context.unionWards.FindAsync(id);
            try
            {
                if (data != null)
                {
                    _context.unionWards.Remove(data);
                    await _context.SaveChangesAsync();
                }
                return status;
            }
            catch (Exception)
            {

                return status = false;
            }

        }
        #endregion



        #region file limits
        public async Task<IEnumerable<FileType>> GetAllFileTypes()
        {
            var data = await _context.fileTypes.Where(x=>x.isActive==true).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<FileLimits>> GetAllFileLimits()
        {
            var data = await _context.fileLimits.Include(x=>x.fileType).ToListAsync();
            return data;
        }

        public FileLimits GetFileLimitsById(int id)
        {
            return _context.fileLimits.FirstOrDefault(fl => fl.Id == id);
        }

        public void AddFileLimits(FileLimits fileLimits)
        {
            _context.fileLimits.Add(fileLimits);
        }

        public void UpdateFileLimits(FileLimits fileLimits)
        {
            _context.fileLimits.Update(fileLimits);
        }

        public void DeactivateAllFileLimits()
        {
            var allFileLimits = _context.fileLimits.ToList();
            foreach (var limit in allFileLimits)
            {
                limit.isActive = false;
            }
        }

        public void SaveFileLimits()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveFileType(FileType fileInfo)
        {
            if (fileInfo.Id != 0)
            {
                _context.fileTypes.Update(fileInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                await _context.fileTypes.AddAsync(fileInfo);
                await _context.SaveChangesAsync();
                return 1;
            }
        }
        #endregion
    }
}
