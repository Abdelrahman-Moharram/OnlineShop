using OnlineShop.Core.DTOs.CartDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;


namespace OnlineShop.Core.IServices
{
    public interface ICartServices 
    {
        Task<GetCartItemsDTO> GetCartByUserId(string userId);
        Task<BaseResponseDTO> UpdateCart(BaseCartItemDTO cartDTO, string userId);
        Task<BaseResponseDTO> DeleteCartItem(string productId, string userId);
    }
}
