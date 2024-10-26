using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public record class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Article { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
