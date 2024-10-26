namespace eShop.Domain.Entities.Cart
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int ProductCount { get; set; }
    }
}
