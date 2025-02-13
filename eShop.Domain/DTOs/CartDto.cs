namespace eShop.Domain.DTOs;

public class CartDto : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}