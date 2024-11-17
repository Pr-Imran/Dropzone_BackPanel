using DropZone_BackPanel.Context;
using DropZone_BackPanel.Contracts;

namespace DropZone_BackPanel.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DropZoneDbContext _context;

        public UnitOfWork(DropZoneDbContext context)
        {
            _context = context;
        }

        //public IGenericRepository<Rank> Ranks
        //    => _ranks ??= new GenericRepository<Rank>(_context);
            
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if(dispose)
            {
                _context.Dispose();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
