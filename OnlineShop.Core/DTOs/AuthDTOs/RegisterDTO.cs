

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.DTOs.AuthDTOs
{
    public class RegisterDTO : LoginDTO
    {

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public IFormFile? Image { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
