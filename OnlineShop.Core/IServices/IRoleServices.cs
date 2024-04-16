using OnlineShop.Core.DTOs;
using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;


namespace OnlineShop.Core.IServices
{
    public interface IRoleServices
    {
        Task<BaseResponseDTO> AddUserToRole(ApplicationUser user, string roleName);


        Task<BaseResponseDTO> AddToRoleAsync(AddUserToRoleDTO addRole);
        Task<BaseResponseDTO> RemoveFromRoleAsync(AddUserToRoleDTO addRole);

        Task<BaseResponseDTO> AddRole(string roleName);
        Task<BaseResponseDTO> RemoveRole(string roleName);
        Task<List<SimpleModule>> AllRoles();
    }
}
