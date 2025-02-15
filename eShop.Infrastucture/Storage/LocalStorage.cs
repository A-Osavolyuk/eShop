﻿namespace eShop.Infrastructure.Storage;

public class LocalStorage(ILocalStorageService localStorageService) : ILocalStorage
{
    private readonly ILocalStorageService localStorageService = localStorageService;

    public async ValueTask ClearAsync() => await localStorageService.ClearAsync();
    public async ValueTask SetCartAsync(CartModel cartModel)
    {
        var key = "cart";
        await localStorageService.SetItemAsync(key, cartModel);
    }

    public async ValueTask<FavoritesModel> ReadFavoritesAsync()
    {
        var key = "favorites";
        if (await localStorageService.ContainKeyAsync(key))
        {
            var favorites = await localStorageService.GetItemAsync<FavoritesModel>(key);

            if (favorites is not null)
            {
                return favorites;
            }
        }

        return new();
    }

    public async ValueTask AddToFavoritesAsync(FavoritesItem item)
    {
        var key = "favorites";
        if (await localStorageService.ContainKeyAsync(key))
        {
            var favorites = await localStorageService.GetItemAsync<FavoritesModel>(key);

            if (favorites is not null)
            {
                favorites.Items.Add(item);
                favorites.Count();
                await localStorageService.SetItemAsync(key, favorites);
            }
        }
        else
        {
            var favorites = new FavoritesModel() { Items = new List<FavoritesItem> { item } };
            favorites.Count();
            await localStorageService.SetItemAsync(key, favorites);
        }
    }

    public async ValueTask RemoveFromFavoritesAsync(string id)
    {
        var key = "favorites";
        if (await localStorageService.ContainKeyAsync(key))
        {
            var favorites = await localStorageService.GetItemAsync<FavoritesModel>(key);

            if (favorites is not null)
            {
                var model = favorites.Items.FirstOrDefault(x => x.ProductId == Guid.Parse(id));

                if (model is not null)
                {
                    favorites.Items.Remove(model);
                    favorites.Count();
                    await localStorageService.SetItemAsync(key, favorites);
                }
            }
        }
    }

    public async ValueTask<bool> IsInFavoriteGoodsAsync(string id)
    {
        var key = "favorites";
        if (await localStorageService.ContainKeyAsync(key))
        {
            var favorites = await localStorageService.GetItemAsync<FavoritesModel>(key);

            if (favorites is not null)
            {
                return favorites.Items.Any(x => x.ProductId == Guid.Parse(id));
            }
        }

        return false;
    }

    public async ValueTask AddToCartAsync(CartItem item)
    {
        var key = "cart";
        var cart = new CartModel();

        if (await localStorageService.ContainKeyAsync(key))
        {
            cart = await localStorageService.GetItemAsync<CartModel>(key);

            if (cart is not null)
            {
                if (cart.Items.Any(x => x.ProductId == item.ProductId))
                {
                    var oldItem = cart.Items.FirstOrDefault(x => x.ProductId == item.ProductId);

                    if (oldItem is not null)
                    {
                        var newItem = new CartItem()
                        {
                            UpdateDate = DateTime.UtcNow,
                            Amount = oldItem.Amount + item.Amount,
                            Article = oldItem.Article,
                            ProductId = oldItem.ProductId
                        };
                        cart.Items.Remove(oldItem);
                        cart.Items.Add(newItem);
                        cart.Count();

                        await localStorageService.SetItemAsync(key, cart);
                    }
                }
                else
                {
                    cart.Items.Add(item);
                    cart.Count();

                    await localStorageService.SetItemAsync(key, cart);
                }
            }
        }
        else
        {
            cart!.Items.Add(item);
            cart.Count();

            await localStorageService.SetItemAsync(key, cart);
        }
    }

    public async ValueTask<bool> IsCartExistsAsync()
    {
        var key = "cart";
        return await localStorageService.ContainKeyAsync(key);
    }

    public async ValueTask CreateCartAsync(CartModel cartModel)
    {
        var key = "cart";
        await localStorageService.SetItemAsync(key, cartModel);
    }

    public async ValueTask<CartModel> ReadCartAsync()
    {
        var key = "cart";
        return (await localStorageService.GetItemAsync<CartModel>(key))!;
    }

    public async ValueTask<int> GetStoreItemsCountAsync()
    {
        var cartKey = "cart";
        var favoritesKey = "favorites";
        var cartCount = 0;
        var favoritesCount = 0;

        if (await localStorageService.ContainKeyAsync(cartKey))
        {
            var cart = await localStorageService.GetItemAsync<CartModel>(cartKey);
            cartCount = cart!.ItemsCount;
        }

        if (await localStorageService.ContainKeyAsync(favoritesKey))
        {
            var favorites = await localStorageService.GetItemAsync<FavoritesModel>(favoritesKey);
            favoritesCount = favorites!.ItemsCount;
        }

        return cartCount + favoritesCount;
    }

    public async ValueTask CreateFavoritesAsync(FavoritesModel favoritesModel)
    {
        var key = "favorites";
        await localStorageService.SetItemAsync(key, favoritesModel);
    }

    public async ValueTask<bool> IsFavoritesExistsAsync()
    {
        var key = "favorites";
        var result = await localStorageService.ContainKeyAsync(key);
        return result;
    }
}