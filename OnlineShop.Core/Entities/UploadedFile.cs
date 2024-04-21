using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Entities
{
    public class UploadedFile : BaseEntity
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }

        public string ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
