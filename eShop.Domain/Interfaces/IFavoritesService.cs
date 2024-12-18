using eShop.Domain.Common.Api;
using eShop.Domain.Requests.CartApi.Favorites;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<Response> GetFavoritesAsync(Guid UserId);
    public ValueTask<Response> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}