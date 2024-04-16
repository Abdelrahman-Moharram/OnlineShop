using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.DTOs.ResponsesDTOs
{
    public class AuthResponseDTO : BaseResponseDTO
    {
        public string UserName { get; set; }    
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
