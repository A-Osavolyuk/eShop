using eShop.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Domain.Entities.Product;

public class Price
{
    [BsonRepresentation(BsonType.String)]
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}