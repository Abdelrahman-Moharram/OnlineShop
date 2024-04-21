using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
namespace OnlineShop.Core.IServices
{
    public interface IAuthServices
    {
        Task<ApplicationUser> AddUser(RegisterDTO userDTO);
        Task<AuthResponseDTO> Register(RegisterDTO userDTO);
        Task<AuthResponseDTO> Login(LoginDTO loginDTO);

        Task<AuthResponseDTO> GenerateNewRefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);

    }
}
