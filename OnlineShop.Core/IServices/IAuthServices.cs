using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
namespace OnlineShop.Core.IServices
{
    public interface IAuthServices
    {
        Task<ApplicationUser> AddUser(RegisterDTO userDTO);
        Task<BaseResponseDTO> Register(RegisterDTO userDTO);
        Task<BaseResponseDTO> Login(LoginDTO loginDTO);
    }
}
