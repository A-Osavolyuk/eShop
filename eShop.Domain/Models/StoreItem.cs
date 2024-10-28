using eShop.Domain.Enums;

namespace eShop.Domain.Models
{
    public class StoreItem
    {
        public Guid ProductId { get; set; }
        public decimal Article { get; set; }
        public Category Category { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
