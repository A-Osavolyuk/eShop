namespace eShop.Domain.Types;

public class FavoritesItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public string Article { get; set; } = String.Empty;
    public DateTime AddedDate { get; set; } = DateTime.Now;
}