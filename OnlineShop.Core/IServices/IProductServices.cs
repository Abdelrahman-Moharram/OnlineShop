using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.IServices
{
    public interface IProductServices
    {
        Task<IEnumerable<ListProductsDTO>> ListProducts();
        Task<BaseResponseDTO> SeedProduct(SeedProductDTO productDTO, string CreatedBy);
        Task<BaseResponseDTO> AddProduct(FormProductDTO productDTO, List<ProductFile> uploadedFiles, string CreatedBy);
    }
}
