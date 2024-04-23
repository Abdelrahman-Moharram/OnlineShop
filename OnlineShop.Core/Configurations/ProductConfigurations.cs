using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.discount)
                .HasDefaultValue(0);

            builder.Property(i => i.Price)
                .HasDefaultValue(0)
                .HasColumnType("money");

            builder.HasOne(i => i.Category)
                .WithMany(ii => ii.Products)
                .HasForeignKey(i => i.CategoryId);

            builder.HasOne(i => i.Brand)
                .WithMany(ii => ii.Products)
                .HasForeignKey(i => i.BrandId);

            

            builder
                .HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
