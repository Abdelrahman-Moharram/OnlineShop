using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Entities
{
    public class ProductItem
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsSelled { get; set; } = false;
        public string Color { get; set; }
        public string SerialNo { get; set; }
    }
}
