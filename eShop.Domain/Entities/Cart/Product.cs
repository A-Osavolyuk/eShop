namespace eShop.Domain.Entities.Cart;

public class Product
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Article { get; set; }
    public int Ammount { get; set; }
}