namespace eShop.Domain.Models
{
    public class FavoriteGoodModel
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal ProductArticle { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
