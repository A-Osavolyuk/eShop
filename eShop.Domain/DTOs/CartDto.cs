using eShop.Domain.Types;

namespace eShop.Domain.DTOs;

public class CartDto
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}