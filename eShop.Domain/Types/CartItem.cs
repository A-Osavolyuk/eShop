namespace eShop.Domain.Types;

public class CartItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public string Article { get; set; } = String.Empty;
    public int Amount { get; set; }
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public DateTime AddedDate { get; set; } = DateTime.Now;
}