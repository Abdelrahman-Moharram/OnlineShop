
namespace OnlineShop.Core.DTOs.CartDTOs
{
    public class GetCartItemsDTO
    {
        public string userId { get; set; }
        public string cartId { get; set; }
        public List<GetCartItemDTO> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
