using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Configurations
{
    public class BrandsConfigurations : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(i => i.CreatedAt)
                    .HasDefaultValueSql("GetDate()");
            builder
                .HasQueryFilter(i => !i.IsDeleted);
        }
    }
}