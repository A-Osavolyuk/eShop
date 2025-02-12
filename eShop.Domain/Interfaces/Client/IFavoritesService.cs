using eShop.Domain.Common.Api;
using eShop.Domain.Requests.Api.Favorites;

namespace eShop.Domain.Interfaces.Client;

public interface IFavoritesService
{
    public ValueTask<Response> GetFavoritesAsync(Guid userId);
    public ValueTask<Response> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}