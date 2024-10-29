using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Domain.Entities.Cart;

public class CartItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public decimal ProductArticle { get; set; }
    public int Amount { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}