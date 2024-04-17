using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;


namespace OnlineShop.Core.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
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