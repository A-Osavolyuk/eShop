using eShop.Domain.Entities.Api.Cart;

namespace eShop.Domain.Requests.Api.Cart;

public record UpdateCartRequest
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}