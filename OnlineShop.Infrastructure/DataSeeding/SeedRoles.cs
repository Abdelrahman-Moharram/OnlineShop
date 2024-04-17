using OnlineShop.Core.Constants;
using OnlineShop.Core.IServices;

namespace OnlineShop.Infrastructure.DataSeeding
{
    public static class SeedRoles
    {
        public static async Task SeedDefaultRolesAsync(IRoleServices roleServices)
        {
            foreach(var role in Enum.GetNames(typeof(Roles)))
            {
                await roleServices.AddRole(role);
            }
        }
    }
}
