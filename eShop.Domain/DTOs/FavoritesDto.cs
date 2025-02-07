using eShop.Domain.Types;

namespace eShop.Domain.DTOs;

public class FavoritesDto
{
    public Guid Id { get; set; }
    public int Cound { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}