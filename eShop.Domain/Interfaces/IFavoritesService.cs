using eShop.Domain.DTOs;
using eShop.Domain.Requests.Cart;

namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<ResponseDTO> GetFavoritesAsync(Guid userId);
    public ValueTask<ResponseDTO> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}