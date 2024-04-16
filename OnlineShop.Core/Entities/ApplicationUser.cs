using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
    }
}
