namespace eShop.Domain.Entities.ProductApi;

public class Price
{
    [BsonRepresentation(BsonType.String)]
    public ProductCurrency ProductCurrency { get; set; }
    public decimal Amount { get; set; }
}