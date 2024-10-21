using eShop.Domain.Enums;

namespace eShop.Domain.Models
{
    public class StoreItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal ProductArticle { get; set; }
        public Category ProductCategory { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
