using OnlineShop.Core.DTOs.BrandDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;


namespace OnlineShop.Core.IServices
{
    public interface IBrandServices
    {
        Task<BaseResponseDTO> AddNew(Brand newBrand, string CreatedBy);
        Task<IEnumerable<GetBrandDTO>> GetAllWithBaseIncludes(int size);
    }
}
