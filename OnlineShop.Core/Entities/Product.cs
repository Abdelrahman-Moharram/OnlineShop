

namespace OnlineShop.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ModelName { get; set; }


        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }

        public string CategoryId { get; set; }
        public Category? Category { get; set; }

        public string BrandId { get; set; }
        public Brand? Brand { get; set; }
        public IEnumerable<ProductItem>? ProductItems { get; set; }
        public IEnumerable<ProductFile>? ProductFiles { get; set; }
        public int Amount { get; set; }
    }
}
