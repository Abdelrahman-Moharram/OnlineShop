

using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Brand> Brands { get; }
        IRepository<Category> Categories { get; }
        IRepository<ProductItem> ProductItems { get; }
        IRepository<UploadedFile> UploadedFiles { get; }
        Task<int> SaveAsync();
    }
}
