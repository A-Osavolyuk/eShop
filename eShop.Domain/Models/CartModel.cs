using eShop.Domain.Entities.Cart;

namespace eShop.Domain.Models
{
    public class CartModel
    {
        public Guid CartId { get; set; }
        public int ItemsCount { get; set; }
        public List<CartItem> Products { get; set; } = new List<CartItem>();

        public void Count()
        {
            ItemsCount = Products.Aggregate(0, (acc, v) => acc + v.Amount);
        }
    }
}
