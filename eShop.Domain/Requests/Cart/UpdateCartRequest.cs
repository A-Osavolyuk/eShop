using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities.Cart;

namespace eShop.Domain.Requests.Cart;

public record UpdateCartRequest : RequestBase
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}