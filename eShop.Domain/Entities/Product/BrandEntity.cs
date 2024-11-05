using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Domain.Entities.Product;

public class BrandEntity
{
    [BsonId, BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}