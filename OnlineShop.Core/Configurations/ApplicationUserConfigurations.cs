using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Entities;


namespace OnlineShop.Core.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasQueryFilter(i => !i.IsDeleted);

            builder.HasOne(i => i.Image)
                .WithOne(ii=>ii.User);

            builder.HasOne(i => i.Cart)
                .WithOne(ii => ii.user);
        }
    }
}
