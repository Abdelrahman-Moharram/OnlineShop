

namespace OnlineShop.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask<int> SaveAsync();
    }
}
