using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;
using OnlineShop.Core.Settings;
using System.Security.Claims;

using OnlineShop.Core.DTOs.ResponsesDTOs;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Constants;

namespace OnlineShop.Services
{
    public class RoleServices : IRoleServices
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleServices> _logger;
        private readonly JWTSettings _jwt;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleServices(
            UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> jwt,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleServices> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _jwt = jwt.Value;


        }

        public async Task<BaseResponseDTO> AddUserToRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return new BaseResponseDTO { Message = "Add To Role Successfully !" };
            }
            _logger.LogError("Something went wrong Add User To Role");
            return new BaseResponseDTO { Message = "Something went wrong !" };
        }

        public async Task<BaseResponseDTO> AddRole(string roleName)
        {
            if (await _roleManager.FindByNameAsync(roleName) != null)
                return new BaseResponseDTO { Message = $"Role {roleName} Already Exists" };

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return new BaseResponseDTO { Message = "Role Added Successfully !", IsSuccessed = true };
            _logger.LogError("Something went wrong Add Role");
            return new BaseResponseDTO { Message = "something went wrong" };
        }

        

        public async Task<BaseResponseDTO> RemoveRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return new BaseResponseDTO { Message = $"Role {roleName} not found" };

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return new BaseResponseDTO { Message = $"Role {roleName} Removed Successfully !", IsSuccessed = true };
            _logger.LogError($"Something went wrong while Adding {roleName} Role");
            return new BaseResponseDTO { Message = "something went wrong" };
        }


        public async Task<BaseResponseDTO> AddToRoleAsync(AddUserToRoleDTO addRole)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(addRole.UserId);

            IdentityRole role = await _roleManager.FindByNameAsync(addRole.RoleName);

            if (user != null && role != null)
            {
                if (await _userManager.IsInRoleAsync(user, addRole.RoleName))
                    return new BaseResponseDTO { Message = "User already assigned to this role" };

                return await AddUserToRole(user, addRole.RoleName);
            }
            return new BaseResponseDTO { Message = "Invalid user or Role" };
        }
        public async Task<BaseResponseDTO> RemoveFromRoleAsync(AddUserToRoleDTO addRole)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(addRole.UserId);

            IdentityRole role = await _roleManager.FindByNameAsync(addRole.RoleName);

            if (user != null && role != null)
            {
                if (!await _userManager.IsInRoleAsync(user, addRole.RoleName))
                    return new BaseResponseDTO { Message = $"{user.UserName} is not  assigned to {addRole.RoleName} role" };

                var result = await _userManager.RemoveFromRoleAsync(user, addRole.RoleName);
                if (result.Succeeded)
                {
                    return new BaseResponseDTO { Message = $"{user.UserName} removed from {addRole.RoleName} role Successfully !", IsSuccessed = true };
                }
            }
            return new BaseResponseDTO { Message = "Invalid user or Role" };
        }




        

        public async Task<List<SimpleModule>> AllRoles()
        {
            return await _roleManager.Roles.Select(i => new SimpleModule { Name = i.Name, Id = i.Id }).ToListAsync();
        }

        public async Task<BaseResponseDTO> AddRoleWithPermissions(string roleName, string[] Permissions)
        {
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new IdentityRole { Name = roleName };
                var result = await _roleManager.CreateAsync(role);
            }

            if (role != null)
            {
                foreach (var permission in Permissions)
                    await _roleManager.AddClaimAsync(role, new Claim(CommonConstantns.Permissions.ToString(), permission));

                return new BaseResponseDTO { Message = $"{roleName} Added Successfully" };
            }

            _logger.LogError($"something went wrong while adding {roleName} Role");
            return new BaseResponseDTO { Message = $"something went wrong while adding {roleName} Role" };
        }


        public async Task<List<string>> GetRoleClaimsPermissions(string roleNameOrRoleId)
        {
            var role = _roleManager.FindByIdAsync(roleNameOrRoleId).Result;
            if (role == null)
                role = _roleManager.FindByNameAsync(roleNameOrRoleId).Result;

            if (role == null)
                return null;

            return _roleManager.GetClaimsAsync(role).Result.Where(i => i.Type == CommonConstantns.Permissions.ToString()).Select(i => i.Value).ToList();
        }


        public async Task<List<string>> EditRoleClaimsPermissions(RolePermissionsDTO permissionsDTO)
        {
            var role = _roleManager.FindByIdAsync(permissionsDTO.RoleId).Result;
            if (role == null)
                return null;
            foreach (var claim in _roleManager.GetClaimsAsync(role).Result)
            {
                if (permissionsDTO.Permissions.FirstOrDefault(p => p.ToLower() == claim.Value.ToLower()) == null)
                    await _roleManager.RemoveClaimAsync(role, claim);
                else
                    permissionsDTO.Permissions.Remove(claim.Value);
            }
            foreach (var permission in permissionsDTO.Permissions)
                await _roleManager.AddClaimAsync(role, new Claim(CommonConstantns.Permissions.ToString(), permission));


            return _roleManager.GetClaimsAsync(role).Result.Where(i => i.Type == CommonConstantns.Permissions.ToString()).Select(i => i.Value).ToList();
        }

    }
}
