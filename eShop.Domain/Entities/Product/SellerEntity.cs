namespace eShop.Domain.Entities.Product;

public class SellerEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Article { get; set; } = string.Empty;
}