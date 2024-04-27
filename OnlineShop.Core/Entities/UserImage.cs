

using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Core.Entities
{
    public class UserImage : BaseEntity
    {
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? FileName { get; set; } = "default.jpg";
        public string? ContentType { get; set; } = "image/jpeg";
    }
}
