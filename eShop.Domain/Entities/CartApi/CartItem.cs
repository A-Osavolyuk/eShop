namespace eShop.Domain.Entities.CartApi;

public class CartItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public string ProductArticle { get; set; } = String.Empty;
    public int Amount { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime AddedAt { get; set; } = DateTime.Now;
}