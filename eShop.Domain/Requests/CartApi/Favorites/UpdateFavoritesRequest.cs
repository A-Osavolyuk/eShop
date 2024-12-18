using eShop.Domain.Entities.CartApi;

namespace eShop.Domain.Requests.CartApi.Favorites;

public record UpdateFavoritesRequest
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}