using eShop.Domain.DTOs.CartApi;
using eShop.Domain.Entities.CartApi;
using eShop.Domain.Requests.CartApi.Cart;

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