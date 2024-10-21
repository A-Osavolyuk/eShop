namespace eShop.Domain.Models
{
    public class CartItem
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal ProductArticle { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
