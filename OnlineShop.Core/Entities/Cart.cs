
namespace OnlineShop.Core.Entities
{
    public class Cart : BaseEntity
    {
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
        
        public bool IsCompleted { get; set; }

        public List<CartItem> CartItems { get; set; }

        public decimal TotalPrice => CartItems.Sum(i=>(i.Product.Price - (i.Product.Price * i.Product.discount / 100)));
    }
}
