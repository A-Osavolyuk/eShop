using eShop.Domain.Models.Store;

namespace eShop.Cart.Api.Mapping;

public static class Mapper
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
    
    public static CartDto ToCartDto(CartEntity entity)
    {
        return new()
        {
            Items = entity.Items,
            CartId = entity.CartId,
            ItemsCount = entity.ItemsCount,
        };
    }

    public static CartStore ToCartModel(CartDto cartDto)
    {
        return new CartStore()
        {
            CartId = cartDto.CartId,
            ItemsCount = cartDto.ItemsCount,
            Items = cartDto.Items
        };
    }

    public static UpdateCartRequest ToUpdateCartRequest(CartStore store)
    {
        return new()
        {
            CartId = store.CartId,
            ItemsCount = store.ItemsCount,
            Items = store.Items
        };
    }
}