using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Favorites;

public record GetFavoritesRequest() : RequestBase
{
    public Guid UserId { get; set; }
}