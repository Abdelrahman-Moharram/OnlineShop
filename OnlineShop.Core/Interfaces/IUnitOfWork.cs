﻿

using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Brand> Brands { get; }
        IRepository<Category> Categories { get; }
        IRepository<ProductItem> ProductItems { get; }
        IRepository<ProductFile> ProductFiles { get; }
        IRepository<Banner> Banners { get; }
        IRepository<SiteSetting> SiteSettings { get; }
        Task<int> SaveAsync();
    }
}
