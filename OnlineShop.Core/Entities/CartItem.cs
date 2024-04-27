
namespace OnlineShop.Core.Entities
{
    public class CartItem: BaseEntity
    {
        public string ProductId { get; set; } 
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public string CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
