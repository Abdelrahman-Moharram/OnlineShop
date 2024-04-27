using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Persistence;
using OnlineShop.Infrastructure.Data;


namespace OnlineShop.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Product> Products { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Brand> Brands { get; private set;}
        public IRepository<ProductItem> ProductItems { get; private set; }
        public IRepository<ProductFile> ProductFiles { get; private set; }
        public IRepository<Banner> Banners { get; private set; }
        public IRepository<UserImage> UserImages { get; private set; }
        public IRepository<SiteSetting> SiteSettings { get; private set; }
        public IRepository<Cart> Carts { get; private set; }
        public IRepository<CartItem> CartItems { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Products = new BaseRepository<Product>(_context);

            Categories = new BaseRepository<Category>(_context);

            Brands = new BaseRepository<Brand>(_context);

            ProductItems = new BaseRepository<ProductItem>(_context);

            ProductFiles = new BaseRepository<ProductFile>(_context);

            Banners = new BaseRepository<Banner>(_context);

            SiteSettings = new BaseRepository<SiteSetting>(_context);

            UserImages = new BaseRepository<UserImage>(_context);

            Carts = new BaseRepository<Cart>(_context);

            CartItems = new BaseRepository<CartItem>(_context);
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
