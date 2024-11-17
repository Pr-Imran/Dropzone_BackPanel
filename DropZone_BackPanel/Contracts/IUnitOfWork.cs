namespace DropZone_BackPanel.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Rank> Ranks { get; }
        
        Task Save();
    }
}
