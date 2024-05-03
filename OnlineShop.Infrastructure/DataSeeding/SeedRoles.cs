using OnlineShop.Core.Constants;
using OnlineShop.Core.IServices;
namespace OnlineShop.Infrastructure.DataSeeding
{
    public static class SeedRoles
    {
        public static async Task SeedDefaultRolesAsync(IRoleServices roleServices)
        {
            foreach (string role in Enum.GetNames(typeof(Roles)))
                foreach (string module in Enum.GetNames(typeof(Modules)))
                    await roleServices.AddRoleWithPermissions(
                        role,
                        Permissions.GeneratePermissionsList(module, RoleModules.instance.cruds(role, module)).ToArray()
                        );
        }
    }
}
