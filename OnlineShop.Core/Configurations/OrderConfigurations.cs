

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(i => i.Customer)
                .WithMany(ii => ii.Orders)
                .HasForeignKey(i => i.CustomerId);

            builder.Property(i => i.TotalPrice)
                   .HasDefaultValue(0)
                   .HasColumnType("money");

            builder
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder
                .HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
