
namespace OnlineShop.Core.DTOs.CartDTOs
{
    public class GetCartItemDTO : BaseCartItemDTO
    {
        public string? ProductName { get; set; }
        public decimal price { get; set; }
        public string? image { get; set; }
    }
}
