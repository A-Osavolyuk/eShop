using eShop.Domain.Entities.CartApi;

namespace eShop.Domain.DTOs.CartApi;

public class CartDto
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}