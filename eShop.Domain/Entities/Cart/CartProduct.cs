namespace eShop.Domain.Entities.Cart;

public class CartProduct
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public int Ammount { get; set; }

    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}