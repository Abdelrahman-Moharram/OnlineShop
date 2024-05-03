
namespace OnlineShop.Core.Entities
{
    public class ProductItem : BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsSelled { get; set; } = false;
        public string SerialNo { get; set; }
        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public OrderItem OrderItem { get; set; }

    }
}
