using DropZone_BackPanel.Areas.Dboard.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.Services.Dapper;
using DropZone_BackPanel.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropZone_BackPanel.Services.MasterData 
{
    public class MasterDataService : IMasterDataService
    {
        private readonly DropSpaceDbContext _context;
        private readonly IDapper _dapper;
        public MasterDataService(DropSpaceDbContext context, IDapper dapper)
        {
            _context = context;
            _dapper = dapper;
        }


        #region Village
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

        public async Task<IEnumerable<Village>> GetAllVillage()
        {
            return await _context.villages.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<Village>> GetAllVillageByUnionId(int id)
        {
            return await _context.villages.Where(x => x.unionWardId == id).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<IEnumerable<Village>> GetAllActiveVillageByUnionId(int id)
        {
            return await _context.villages.Where(x => x.unionWardId == id && x.isActive != "Inactive").OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<IEnumerable<Village>> GetAllVillageByThanaUnionId(int thana, int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.villages.Where(x => x.unionWard.thanaId == thana && x.isActive != "Inactive").OrderByDescending(x => x.Id).ToListAsync();
            }
            else
            {
                return await _context.villages.Where(x => x.unionWard.thanaId == thana && x.isActive != "Inactive").Where(x => x.unionWardId == id).OrderByDescending(x => x.Id).ToListAsync();
            }
        }

        public async Task<IEnumerable<object>> GetVillageList()
        {
            try
            {
                var result = (from V in _context.villages
                              join U in _context.unionWards on V.unionWardId equals U.Id
                              join T in _context.thanas on U.thanaId equals T.Id
                              join DS in _context.districts on T.districtId equals DS.Id
                              join D in _context.divisions on DS.divisionId equals D.Id
                              select new
                              {
                                  V.Id,
                                  V.unionWardId,
                                  DS.divisionId,
                                  D.divisionName,
                                  DS.districtName,
                                  T.thanaName,
                                  U.unionName,
                                  V.villageCode,
                                  V.villageName,
                                  V.villageNameBn,
                                  V.isActive
                              }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteVillageById(int id)
        {
            _context.villages.Remove(await _context.villages.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }


        //public async Task<IEnumerable<VillageListViewModel>> GetVillageListByFilter(int? divId, int? distId, int? thanaId, int? unionId)
        //{
        //    var data = await _dapper.FromSqlAsync<VillageListViewModel>($"SP_GET_VillageListByFilter {divId},{distId},{thanaId},{unionId}");
        //    //var data = await _context.VillageListViewModels.FromSql($"SP_GET_VillageListByFilter {divId},{distId},{thanaId},{unionId}").OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        //    return data.OrderByDescending(x => x.Id).ToList();
        //}

        #endregion

        #region Division
        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            return await _context.divisions.AsNoTracking().ToListAsync();
        }

        public async Task<Division> GetDivisionById(int id)
        {
            return await _context.divisions.FindAsync(id);
        }

        public async Task<bool> SaveDivision(Division division)
        {
            if (division.Id != 0)
                _context.divisions.Update(division);
            else
                _context.divisions.Add(division);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDivisionById(int id)
        {
            _context.divisions.Remove(_context.divisions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            return await _context.divisions.Where(X => X.countryId == CntId).ToListAsync();
        }

        #endregion
    }
}
