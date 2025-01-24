using eShop.Domain.Entities.Api.Cart;

namespace eShop.Domain.DTOs.Api.Cart;

public class CartDto
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}