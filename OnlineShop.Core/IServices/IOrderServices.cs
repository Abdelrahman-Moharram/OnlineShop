
using OnlineShop.Core.DTOs.OrderDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;

namespace OnlineShop.Core.IServices
{
    public interface IOrderServices
    {
        Task<BaseResponseDTO> PlaceOrder(string userId);
        Task<BaseResponseDTO> NewOrder(AddOrderDTO orderDTO, string CreatedBy);
    }
}
