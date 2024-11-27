namespace eShop.Domain.Interfaces;

public interface IFavoritesService
{
    public ValueTask<ResponseDto> GetFavoritesAsync(Guid UserId);
    public ValueTask<ResponseDto> UpdateFavoritesAsync(UpdateFavoritesRequest request);
}