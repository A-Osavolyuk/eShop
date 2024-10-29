using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities.Cart;

namespace eShop.Domain.Requests.Cart;

public record UpdateFavoritesRequest : RequestBase
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}