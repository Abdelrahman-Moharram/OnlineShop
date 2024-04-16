using OnlineShop.Core.Interfaces;
using OnlineShop.Infrastructure.Data;


namespace OnlineShop.Infrastructure.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async ValueTask<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
