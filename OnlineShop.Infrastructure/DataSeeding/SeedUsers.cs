using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.DataSeeding
{

    public static class SeedUsers
    {
        
        public static async Task SeedDefaultUsersAsync(IAuthServices authServices, IRoleServices roleServices) 
        {
            // Seed Basic User

            ApplicationUser basic =  await authServices.AddUser(new RegisterDTO
            {
                Email = "basicuser@site.com",
                Username = "basic-user",
                Password = "P@ssw0rd",
            });

            await roleServices.AddUserToRole(basic, "Basic");

            // Seed Admin User

            ApplicationUser admin =  await authServices.AddUser(new RegisterDTO
            {
                Email = "adminuser@site.com",
                Username = "admin-user",
                Password = "P@ssw0rd",
            });

            await roleServices.AddUserToRole(admin, "Admin");

            // Seed Admin User

            ApplicationUser superadmin =  await authServices.AddUser(new RegisterDTO
            {
                Email = "superadminuser@site.com",
                Username = "superadmin-user",
                Password = "P@ssw0rd",
            });

            await roleServices.AddUserToRole(superadmin, "SuperAdmin");
        }
    }
}
