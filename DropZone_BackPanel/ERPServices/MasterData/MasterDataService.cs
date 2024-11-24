using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;
using DropZone_BackPanel.ERPServices.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropZone_BackPanel.ERPServices.MasterData
{
    public class MasterDataService:IMasterData
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
            return await _context.villages.Where(x => x.unionWardId == id).OrderByDescending(x => x.Id).ToListAsync();
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
    }
}
