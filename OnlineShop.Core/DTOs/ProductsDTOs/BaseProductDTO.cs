

namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class BaseProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }
        public decimal PriceAfterDiscount => Price - (Price * discount / 100);
    }
}
