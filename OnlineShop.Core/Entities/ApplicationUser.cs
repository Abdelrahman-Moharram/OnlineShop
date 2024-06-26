﻿using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserImage? Image { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<Order>? Orders { get; set; }
        
        public Cart? Cart { get; set; }
        public bool IsDeleted { get; set; }
    }
}
