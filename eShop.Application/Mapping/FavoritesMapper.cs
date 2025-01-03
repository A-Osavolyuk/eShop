using eShop.Domain.DTOs.CartApi;
using eShop.Domain.Entities.CartApi;
using eShop.Domain.Models.Store;
using eShop.Domain.Requests.CartApi.Favorites;

namespace eShop.Application.Mapping;

public static class FavoritesMapper
{
    public static FavoritesDto ToFavoritesDto(FavoritesEntity entity)
    {
        return new()
        {
            Items = entity.Items,
            FavoritesId = entity.FavoritesId,
            ItemsCount = entity.ItemsCount,
        };
    }

    public static FavoritesStore ToFavoritesModel(FavoritesDto dto)
    {
        return new()
        {
            Items = dto.Items,
            FavoritesId = dto.FavoritesId,
            ItemsCount = dto.ItemsCount,
        };
    }

    public static UpdateFavoritesRequest ToUpdateFavoritesRequest(FavoritesStore store)
    {
        return new()
        {
            Items = store.Items,
            FavoritesId = store.FavoritesId,
            ItemsCount = store.ItemsCount,
        };
    }
}