

using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Core.Entities
{
    public class UserImage
    {
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? FileName { get; set; } = "default.jpg";
        public string? ContentType { get; set; } = "image/jpeg";
    }
}
