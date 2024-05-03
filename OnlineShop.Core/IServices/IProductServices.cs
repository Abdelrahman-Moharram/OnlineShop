using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;


namespace OnlineShop.Core.IServices
{
    public interface IProductServices
    {
        Task<ProductPageDTO> ListProducts(int take, int skip, string? sort, decimal? minprice, decimal? maxprice);
        Task<ProductDetailsDTO> ProductDetails(string Id);
        Task<IEnumerable<ListProductsDTO>> GetProductByCategoryIdOrBrandId(string Id, string productId);
        Task<IEnumerable<ListProductsDTO>> Search(string query, int take = 5);
        Task<BaseResponseDTO> SeedProduct(SeedProductDTO productDTO, string CreatedBy);
        Task<BaseResponseDTO> SeedProductItem(string createdBy);
        Task<BaseResponseDTO> AddProduct(FormProductDTO productDTO, List<ProductFile> uploadedFiles, string CreatedBy);
    }
}
