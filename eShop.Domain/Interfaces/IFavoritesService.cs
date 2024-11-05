using eShop.Domain.DTOs;
using eShop.Domain.Requests.Favorites;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<ResponseDTO> GetFavoritesAsync(Guid UserId);
    public ValueTask<ResponseDTO> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}