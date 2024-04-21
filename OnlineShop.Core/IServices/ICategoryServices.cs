using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.IServices
{
    public interface ICategoryServices
    {
        Task<BaseResponseDTO> AddNew(Category newCategory, string CreatedBy);
    }
}
