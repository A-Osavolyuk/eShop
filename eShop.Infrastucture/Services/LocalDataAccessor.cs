﻿using Blazored.LocalStorage;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces;
using eShop.Domain.Models;
using Cart = eShop.Domain.Models.Cart;

namespace eShop.Infrastructure.Services
{
    public class LocalDataAccessor(ILocalStorageService localStorageService) : ILocalDataAccessor
    {
        private readonly ILocalStorageService localStorageService = localStorageService;

        public async ValueTask RemoveDataAsync() => await localStorageService.ClearAsync();

        public async ValueTask SetCartAsync(CartDTO Cart) => await localStorageService.SetItemAsync("cart", Cart);

        public async ValueTask<CartDTO> GetCartAsync() => (await localStorageService.GetItemAsync<CartDTO>("cart"))!;

        public async ValueTask WriteUserDataAsync(UserDataModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await localStorageService.SetItemAsync("user", user);
        }

        public async ValueTask<UserDataModel> ReadUserDataAsync()
        {
            var user = await localStorageService.GetItemAsync<UserDataModel>("user");

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async ValueTask WritePersonalDataAsync(PersonalDataModel personalData)
        {
            if (personalData is null)
            {
                throw new ArgumentNullException(nameof(personalData));
            }

            await localStorageService.SetItemAsync("personal-data", personalData);
        }

        public async ValueTask<PersonalDataModel> ReadPersonalDataAsync()
        {
            var personalData = await localStorageService.GetItemAsync<PersonalDataModel>("personal-data");

            if (personalData is null)
            {
                return new();
            }

            return personalData;
        }

        public async ValueTask WriteSecurityDataAsync(SecurityDataModel securityData)
        {
            if (securityData is null)
            {
                throw new ArgumentNullException(nameof(securityData));
            }

            await localStorageService.SetItemAsync("security-data", securityData);
        }

        public async ValueTask<SecurityDataModel> ReadSecurityDataAsync()
        {
            var securityData = await localStorageService.GetItemAsync<SecurityDataModel>("security-data");

            if (securityData is null)
            {
                return new();
            }

            return securityData;
        }

        public async ValueTask WriteAvatarLinkAsync(string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                throw new ArgumentNullException(nameof(link));
            }

            await localStorageService.SetItemAsStringAsync("avatar-link", link);
        }

        public async ValueTask<string> ReadAvatarLinkAsync()
        {
            return await localStorageService.GetItemAsStringAsync("avatar-link") ?? string.Empty;
        }

        public async ValueTask RemoveAvatarLinkAsync()
        {
            await localStorageService.RemoveItemAsync("avatar-link");
        }

        public async ValueTask<Favorites> ReadFavoriteGoodsAsync()
        {
            var key = "favorites";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var favorites = await localStorageService.GetItemAsync<Favorites>(key);

                if (favorites is not null)
                {
                    return favorites;
                }
            }

            return new();
        }

        public async ValueTask AddoFavoritesAsync(StoreItem item)
        {
            var key = "favorites";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var favorites = await localStorageService.GetItemAsync<Favorites>(key);

                if (favorites is not null)
                {
                    favorites.Products.Add(item);
                    favorites.Count();
                    await localStorageService.SetItemAsync(key, favorites);
                }
            }
            else
            {
                var favorites = new Favorites() { Products = new List<StoreItem> { item } };
                favorites.Count();
                await localStorageService.SetItemAsync(key, favorites);
            }
        }

        public async ValueTask RemoveFromFavoritesAsync(string id)
        {
            var key = "favorites";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var favorites = await localStorageService.GetItemAsync<Favorites>(key);

                if (favorites is not null)
                {
                    var model = favorites.Products.FirstOrDefault(x => x.ProductId == id);

                    if (model is not null)
                    {
                        favorites.Products.Remove(model);
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
                var favorites = await localStorageService.GetItemAsync<Favorites>(key);

                if (favorites is not null)
                {
                    return favorites.Products.Any(x => x.ProductId == id);
                }
            }

            return false;
        }

        public async ValueTask AddToCartAsync(StoreItem item)
        {
            var key = "cart";
            var cart = new Cart();

            if (await localStorageService.ContainKeyAsync(key))
            {
                cart = await localStorageService.GetItemAsync<Cart>(key);

                if (cart is not null)
                {
                    if (cart.Products.Any(x => x.ProductId == item.ProductId))
                    {
                        var oldItem = cart.Products.FirstOrDefault(x => x.ProductId == item.ProductId);

                        if (oldItem is not null)
                        {
                            var newItem = new StoreItem()
                            {
                                AddedAt = DateTime.UtcNow,
                                Amount = oldItem.Amount + item.Amount,
                                ProductArticle = oldItem.ProductArticle,
                                ProductId = oldItem.ProductId
                            };
                            cart.Products.Remove(oldItem);
                            cart.Products.Add(newItem);
                            cart.Count();

                            await localStorageService.SetItemAsync(key, cart);
                        }
                    }
                    else
                    {
                        cart.Products.Add(item);
                        cart.Count();

                        await localStorageService.SetItemAsync(key, cart);
                    }
                }
            }
            else
            {
                cart!.Products.Add(item);
                cart.Count();

                await localStorageService.SetItemAsync(key, cart);
            }
        }

        public async ValueTask<bool> IsCartExistsAsync()
        {
            var key = "cart";
            return await localStorageService.ContainKeyAsync(key);
        }

        public async ValueTask CreateCartAsync(Cart cart)
        {
            var key = "cart";
            await localStorageService.SetItemAsync(key, cart);
        }

        public async ValueTask<Cart> ReadCartAsync()
        {
            var key = "cart";
            return (await localStorageService.GetItemAsync<Cart>(key))!;
        }

        public async ValueTask<int> GetStoreItemsCountAsync()
        {
            var cartKey = "cart";
            var favoritesKey = "favorites";
            var cartCount = 0;
            var favoritesCount = 0;

            if (await localStorageService.ContainKeyAsync(cartKey))
            {
                var cart = await localStorageService.GetItemAsync<Cart>(cartKey);
                cartCount = cart!.ItemsCount;
            }

            if (await localStorageService.ContainKeyAsync(favoritesKey))
            {
                var favorites = await localStorageService.GetItemAsync<Favorites>(favoritesKey);
                favoritesCount = favorites!.ItemsCount;
            }

            return cartCount + favoritesCount;
        }

        public async ValueTask CreateFavoritesAsync(Favorites favorites)
        {
            var key = "favorites";
            await localStorageService.SetItemAsync(key, favorites);
        }

        public async ValueTask<bool> IsFavoritesExistsAsync()
        {
            var key = "favorites";
            return await localStorageService.ContainKeyAsync(key);
        }
    }
}