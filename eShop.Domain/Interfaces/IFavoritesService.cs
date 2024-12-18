using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<Response> GetFavoritesAsync(Guid UserId);
    public ValueTask<Response> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}