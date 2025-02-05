using eShop.Domain.Types;

namespace eShop.Domain.DTOs;

public class FavoritesDto
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}