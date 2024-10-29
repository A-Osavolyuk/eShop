using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Domain.Entities.Cart;

public class Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid CartId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public int ItemsCount { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}