using eShop.Domain.Types;

namespace eShop.Cart.Api.Data.Entities;

public class FavoritesEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid FavoritesId { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid UserId { get; set; }
    public int ItemsCount { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}