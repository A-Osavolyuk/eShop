using eShop.Domain.Entities.CartApi;

namespace eShop.Domain.DTOs.CartApi;

public class FavoritesDto
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}