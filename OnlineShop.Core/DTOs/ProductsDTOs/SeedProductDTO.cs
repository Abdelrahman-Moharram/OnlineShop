
namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class SeedProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ModelName { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string BrandId { get; set; }
        public string Brand {  get; set; }

        public string Category { get; set; }

        public List<string>? Images { get; set; }
    }
}
