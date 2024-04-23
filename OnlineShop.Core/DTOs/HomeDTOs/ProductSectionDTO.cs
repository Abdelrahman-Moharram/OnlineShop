using OnlineShop.Core.DTOs.ProductsDTOs;


namespace OnlineShop.Core.DTOs.HomeDTOs
{
    public class ProductSectionDTO 
    {
        public string Title { get; set; }
        public List<ListProductsDTO> ProductsList { get; set; }
    }
}
