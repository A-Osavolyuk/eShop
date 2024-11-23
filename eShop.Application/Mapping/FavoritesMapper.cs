using eShop.Domain.Requests.Favorites;

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

    public static FavoritesModel ToFavoritesModel(FavoritesDto dto)
    {
        return new()
        {
            Items = dto.Items,
            FavoritesId = dto.FavoritesId,
            ItemsCount = dto.ItemsCount,
        };
    }

    public static UpdateFavoritesRequest ToUpdateFavoritesRequest(FavoritesModel model)
    {
        return new()
        {
            Items = model.Items,
            FavoritesId = model.FavoritesId,
            ItemsCount = model.ItemsCount,
        };
    }
}