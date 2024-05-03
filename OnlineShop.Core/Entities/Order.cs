
namespace OnlineShop.Core.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public virtual IEnumerable<ProductItem>? ProductItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

    }
}
