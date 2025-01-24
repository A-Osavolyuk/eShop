using eShop.Domain.DTOs.Api.Cart;
using eShop.Domain.Entities.Api.Cart;
using eShop.Domain.Models.Store;
using eShop.Domain.Requests.Api.Cart;

namespace eShop.Application.Mapping;

public static class CartMapper
{
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