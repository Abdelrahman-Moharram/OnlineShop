using OnlineShop.Core.DTOs.HomeDTOs;


namespace OnlineShop.Core.IServices
{
    public interface IHomeServices
    {
        Task<IndexDTO> GetHomePageData();
    }
}
