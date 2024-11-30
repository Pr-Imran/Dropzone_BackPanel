using DropZone_BackPanel.Data.Entity.Droper;
using DropZone_BackPanel.Data.Entity.MasterData;
using DropZone_BackPanel.Data.Entity.MasterData.PublicMapping;

namespace DropZone_BackPanel.ERPServices.MasterData.Interfaces
{
    public interface IMasterData
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);
        Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId);
        Task<IEnumerable<Thana>> GetActiveThanasByDistrictId(int DistrictId);
        Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId);
        Task<IEnumerable<UnionWard>> GetActiveUnionWardsByThanaId(int thanaId);
        Task<int> SaveVillage(Village village);
        Task<IEnumerable<Village>> GetAllVillageByUnionId(int id);
        Task<IEnumerable<Village>> GetAllActiveVillageByUnionId(int id);
        Task<bool> DeleteVillageById(int id);

        #region Crime Types
        Task<IEnumerable<CrimeInfo>> GetAllCrimeTypes();
        Task<int> SaveCrimeType(CrimeInfo crimeInfo);
        Task<bool> DeleteCrimeTypeById(int id);
        #endregion

        #region UnionWard
        Task<IEnumerable<UnionWard>> GetAllUnionWards();
        Task<IEnumerable<Thana>> GetAllThanas();
        Task<int> SaveUnionWards(UnionWard unionWard);
        Task<bool> DeleteUnionWardById(int id);
        #endregion
        #region filelimits
        Task<IEnumerable<FileType>> GetAllFileTypes();
        Task<IEnumerable<FileLimits>> GetAllFileLimits();

        FileLimits GetFileLimitsById(int id);
        void AddFileLimits(FileLimits fileLimits);
        void UpdateFileLimits(FileLimits fileLimits);
        void DeactivateAllFileLimits();
        void SaveFileLimits();
        Task<int> SaveFileType(FileType fileInfo);
        #endregion
    }


}
