using eShop.Domain.DTOs;
using eShop.Domain.Requests.Favorites;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<ResponseDto> GetFavoritesAsync(Guid UserId);
    public ValueTask<ResponseDto> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}