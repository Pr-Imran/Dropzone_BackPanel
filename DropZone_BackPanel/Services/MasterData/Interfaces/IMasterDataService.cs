using DropZone_BackPanel.Areas.Dboard.Models;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;

namespace DropZone_BackPanel.Services.MasterData.Interfaces
{
    public interface IMasterDataService
    {
        #region Village

        Task<int> SaveVillage(Village village);
        Task<IEnumerable<Village>> GetAllVillage();
        Task<IEnumerable<Village>> GetAllVillageByUnionId(int id);
        Task<IEnumerable<Village>> GetAllActiveVillageByUnionId(int id);
        Task<IEnumerable<System.Object>> GetVillageList();
        Task<bool> DeleteVillageById(int id);
        Task<IEnumerable<Village>> GetAllVillageByThanaUnionId(int thana, int? id);
        //Task<IEnumerable<VillageListViewModel>> GetVillageListByFilter(int? divId, int? distId, int? thanaId, int? unionId);
        #endregion

        #region Division
        Task<bool> SaveDivision(Division division);
        Task<IEnumerable<Division>> GetAllDivision();
        Task<Division> GetDivisionById(int id);
        Task<bool> DeleteDivisionById(int id);
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);

        #endregion

    }
}
