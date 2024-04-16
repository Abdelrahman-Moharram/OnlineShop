using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.DTOs.ResponsesDTOs
{
    public class BaseResponseDTO
    {
        public bool IsSuccessed { get; set; }

        public string? Message { get; set; }
    }
}
