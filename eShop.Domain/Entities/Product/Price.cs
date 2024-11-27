namespace eShop.Domain.Entities.Product;

public class Price
{
    [BsonRepresentation(BsonType.String)]
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}