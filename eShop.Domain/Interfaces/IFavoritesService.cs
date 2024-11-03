using eShop.Domain.DTOs;
using eShop.Domain.Requests.Cart;
using eShop.Domain.Requests.Favorites;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<ResponseDTO> GetFavoritesAsync(GetFavoritesRequest request);
    public ValueTask<ResponseDTO> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}