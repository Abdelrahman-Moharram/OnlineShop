
namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class ProductPageDTO
    {
        public IEnumerable<ListProductsDTO> ProductList { get; set; }
        public int? pages { get; set; } = 0;

    }
}
