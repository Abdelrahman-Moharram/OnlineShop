using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Entities
{
    public class ProductFile:UploadedFile
    {
        public string ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
