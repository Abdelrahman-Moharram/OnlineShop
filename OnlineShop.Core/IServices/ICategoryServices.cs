using OnlineShop.Core.DTOs.CategoryDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using System;

namespace OnlineShop.Core.IServices
{
    public interface ICategoryServices
    {
        Task<BaseResponseDTO> AddNew(Category newCategory, string CreatedBy);
        Task<IEnumerable<GetCategoryDTO>> GetAllWithBaseIncludes(int size);
    }
}
