using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
namespace OnlineShop.Core.IServices
{
    public interface IAdminServices
    {
        Task<BaseResponseDTO> AddBannerImages(List<Banner> Banners, string userId);
    }
}
