﻿using Microsoft.AspNetCore.Http;


namespace OnlineShop.Core.DTOs.ProductsDTOs
{
    public class FormProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<string>? Image { get; set; }
        public string ModelName { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string BrandId { get; set; }

        public List<IFormFile>? Files { get; set; }

    }
}
