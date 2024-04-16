using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required, MinLength(2)]
        public string Username { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
