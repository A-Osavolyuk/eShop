namespace eShop.Domain.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int ProductCount { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
    }
}
