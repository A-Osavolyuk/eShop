namespace eShop.Domain.DTOs;

public class FavoritesDto : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}