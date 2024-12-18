namespace eShop.Domain.Entities.ProductApi;

public class Price
{
    [BsonRepresentation(BsonType.String)]
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}