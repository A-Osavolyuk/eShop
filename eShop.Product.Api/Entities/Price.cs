using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Product.Api.Entities;

public class Price
{
    [BsonRepresentation(BsonType.String)] public ProductCurrency ProductCurrency { get; set; }
    public decimal Amount { get; set; }
}