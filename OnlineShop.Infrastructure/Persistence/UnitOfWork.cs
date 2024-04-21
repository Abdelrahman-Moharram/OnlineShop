using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Persistence;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Product> Products { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Brand> Brands { get; private set;}
        public IRepository<ProductItem> ProductItems { get; private set; }
        public IRepository<UploadedFile> UploadedFiles { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Products = new BaseRepository<Product>(_context);

            Categories = new BaseRepository<Category>(_context);

            Brands = new BaseRepository<Brand>(_context);

            ProductItems = new BaseRepository<ProductItem>(_context);

            UploadedFiles = new BaseRepository<UploadedFile>(_context);
            
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
