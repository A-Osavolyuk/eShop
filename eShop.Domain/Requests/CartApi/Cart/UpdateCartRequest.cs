using eShop.Domain.Entities.CartApi;

namespace eShop.Domain.Requests.CartApi.Cart;

public record UpdateCartRequest
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}