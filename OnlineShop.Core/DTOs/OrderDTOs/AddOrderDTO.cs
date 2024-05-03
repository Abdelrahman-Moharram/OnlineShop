using OnlineShop.Core.DTOs.CartDTOs;

namespace OnlineShop.Core.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public List<BaseCartItemDTO> cartItems {  get; set; }
    }
}
