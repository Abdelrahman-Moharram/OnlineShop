using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Configurations;
using OnlineShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            /*
            new CategoryConfigurations().Configure(builder.Entity<Category>());
            new ProductConfigurations().Configure(builder.Entity<Product>());

            // ProductsInventoryConfigurations -> included in ProductConfigurations 

            new ProductItemConfigurations().Configure(builder.Entity<ProductItem>());
            new BrandsConfigurations().Configure(builder.Entity<Brand>());

            builder.Entity<UploadedFile>()
                .HasOne(i => i.Product)
                .WithMany(i => i.UploadedFiles)
                .HasForeignKey(i => i.ProductId);

            builder.Entity<UploadedFile>()
                .HasQueryFilter(i => !i.IsDeleted);*/

        }

        /*DbSet<Brand> Brands { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductItem> ProductItems { get; set; }
        DbSet<UploadedFile> UploadedFiles { get; set; }*/
    }
}
