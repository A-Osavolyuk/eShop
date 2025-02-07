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
            Id = entity.FavoritesId,
            Cound = entity.ItemsCount,
        };
    }

    public static FavoritesModel ToFavoritesModel(FavoritesDto dto)
    {
        return new()
        {
            Items = dto.Items,
            FavoritesId = dto.Id,
            ItemsCount = dto.Cound,
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
            Id = entity.CartId,
            Count = entity.ItemsCount,
        };
    }

    public static CartModel ToCartModel(CartDto cartDto)
    {
        return new CartModel()
        {
            CartId = cartDto.Id,
            ItemsCount = cartDto.Count,
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