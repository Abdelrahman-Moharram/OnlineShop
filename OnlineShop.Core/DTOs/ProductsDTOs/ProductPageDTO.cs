
namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class ProductPageDTO
    {
        public IEnumerable<ListProductsDTO> ProductList { get; set; }
        public int? pages { get; set; } = 0;
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set;}

    }
}
