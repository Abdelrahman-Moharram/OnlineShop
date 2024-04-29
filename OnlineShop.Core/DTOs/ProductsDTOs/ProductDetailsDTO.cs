
namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class ProductDetailsDTO : BaseProductDTO
    {
        public List<string> Image { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string BrandId { get; set; }
        public string BrandName { get; set;}
        public int Amount { get; set; }
        public string Description { get; set; }

        public string ModelName { get; set; }
    }
}
