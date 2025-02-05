using eShop.Domain.DTOs;
using eShop.Domain.Models;

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
    
    public static CartDto ToCartDto(CartEntity entity)
    {
        return new()
        {
            Items = entity.Items,
            CartId = entity.CartId,
            ItemsCount = entity.ItemsCount,
        };
    }

    public static CartModel ToCartModel(CartDto cartDto)
    {
        return new CartModel()
        {
            CartId = cartDto.CartId,
            ItemsCount = cartDto.ItemsCount,
            Items = cartDto.Items
        };
    }

    public static UpdateCartRequest ToUpdateCartRequest(CartModel model)
    {
        return new()
        {
            CartId = model.CartId,
            ItemsCount = model.ItemsCount,
            Items = model.Items
        };
    }
}