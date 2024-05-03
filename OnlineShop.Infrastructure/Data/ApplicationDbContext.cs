using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Configurations;
using OnlineShop.Core.Entities;


namespace OnlineShop.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Identity Tabels
            builder.Entity<ApplicationUser>().ToTable("Users", schema: "Identity");
            builder.Entity<IdentityRole>().ToTable("Roles", schema: "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schema: "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", schema: "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", schema: "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", schema: "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", schema: "Identity");



            // Models Configurations

            new ApplicationUserConfigurations().Configure(builder.Entity<ApplicationUser>());

            new CategoryConfigurations().Configure(builder.Entity<Category>());
            new ProductConfigurations().Configure(builder.Entity<Product>());
            new OrderConfigurations().Configure(builder.Entity<Order>());

            // ProductsInventoryConfigurations -> included in ProductConfigurations 

            new ProductItemConfigurations().Configure(builder.Entity<ProductItem>());
            new BrandsConfigurations().Configure(builder.Entity<Brand>());

            builder.Entity<OrderItem>()
                .HasQueryFilter(i => !i.IsDeleted);


            builder.Entity<ProductFile>()
                .HasOne(i => i.Product)
                .WithMany(i => i.ProductFiles)
                .HasForeignKey(i => i.ProductId);

            builder.Entity<ProductFile>()
                .HasQueryFilter(i => !i.IsDeleted);

            builder.Entity<Banner>()
                .HasQueryFilter(i => !i.IsDeleted);

            builder.Entity<SiteSetting>()
                .HasQueryFilter(i => !i.IsDeleted);


            builder.Entity<Cart>()
                .HasMany(i => i.CartItems)
                .WithOne(ii => ii.Cart)
                .HasForeignKey(iii=>iii.CartId);

            builder.Entity<CartItem>().HasQueryFilter(i => !i.IsDeleted);
            builder.Entity<Cart>().HasQueryFilter(i => !i.IsDeleted);

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
/*        public DbSet<UserImage> UsersImages { get; set; }*/

        public DbSet<SiteSetting> SiteSettings { get; set; }  
        public DbSet<Cart> Carts { get; set; }  
        public DbSet<CartItem> CartItems { get; set; }
    }
}
