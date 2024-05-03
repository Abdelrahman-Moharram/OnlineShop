
namespace OnlineShop.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; }
        public string ProductItemId { get; set; }        
        public ProductItem ProductItem { get; set; }
        public Order Order { get; set; }

    }
}
