namespace eShop.Domain.Requests.Cart;

public record UpdateCartRequest : RequestBase
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}