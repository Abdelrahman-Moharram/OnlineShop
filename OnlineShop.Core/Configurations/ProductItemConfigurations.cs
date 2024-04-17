using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Configurations
{
    public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {


            builder.HasKey(
                i => i.Id);


            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");



            builder
                .HasOne(i => i.Product)
                .WithMany(ii => ii.ProductItems)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasQueryFilter(i => !i.IsDeleted)
                .HasQueryFilter(ii => !ii.IsSelled);

        }
    }
}