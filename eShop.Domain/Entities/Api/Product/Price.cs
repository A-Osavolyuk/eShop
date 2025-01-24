namespace eShop.Domain.Entities.Api.Product;

public class Price
{
    [BsonRepresentation(BsonType.String)]
    public ProductCurrency ProductCurrency { get; set; }
    public decimal Amount { get; set; }
}