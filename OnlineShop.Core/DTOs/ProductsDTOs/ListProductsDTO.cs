
namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class ListProductsDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<string>? Image {  get; set; }
    }
}
