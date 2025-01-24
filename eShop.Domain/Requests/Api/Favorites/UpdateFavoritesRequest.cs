using eShop.Domain.Entities.Api.Cart;

namespace eShop.Domain.Requests.Api.Favorites;

public record UpdateFavoritesRequest
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}