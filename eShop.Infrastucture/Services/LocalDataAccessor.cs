using Blazored.LocalStorage;
using eShop.Domain.DTOs;
using eShop.Domain.Interfaces;
using eShop.Domain.Models;
using LanguageExt.Pipes;

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

        public async ValueTask<IEnumerable<FavoriteGoodModel>> ReadFavoriteGoodsAsync()
        {
            var key = "favorite-goods";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var list = await localStorageService.GetItemAsync<List<FavoriteGoodModel>>(key);

                if (list is not null && list.Any())
                {
                    return list;
                }
            }

            return Enumerable.Empty<FavoriteGoodModel>();
        }

        public async ValueTask AddFavoriteGoodAsync(FavoriteGoodModel model)
        {
            var key = "favorite-goods";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var list = await localStorageService.GetItemAsync<List<FavoriteGoodModel>>(key);

                if (list is not null)
                {
                    list.Add(model);
                    await localStorageService.SetItemAsync(key, list);
                }
            }
            else
            {
                var list = new List<FavoriteGoodModel>() { model };
                await localStorageService.SetItemAsync(key, list);
            }
        }

        public async ValueTask RemoveFavoriteGoodAsync(string id)
        {
            var key = "favorite-goods";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var list = await localStorageService.GetItemAsync<List<FavoriteGoodModel>>(key);

                if (list is not null && list.Any())
                {
                    var model = list.FirstOrDefault(x => x.ProductId == id);

                    if (model is not null)
                    {
                        list.Remove(model);
                        await localStorageService.SetItemAsync(key, list);
                    }
                }
            }
        }

        public async ValueTask<bool> IsInFavoriteGoodsAsync(string id)
        {
            var key = "favorite-goods";
            if (await localStorageService.ContainKeyAsync(key))
            {
                var list = await localStorageService.GetItemAsync<List<FavoriteGoodModel>>(key);

                if (list is not null && list.Any())
                {
                    return list.Any(x => x.ProductId == id);

                }
            }

            return false;
        }

        public async ValueTask AddToCartAsync(CartItem item)
        {
            var key = "cart";
            var cart = new Cart();

            if (await localStorageService.ContainKeyAsync(key))
            {
                cart = await localStorageService.GetItemAsync<Cart>(key);

                if(cart is not null)
                {
                    if (cart.Products.Any(x => x.ProductId == item.ProductId))
                    {
                        var oldItem = cart.Products.FirstOrDefault(x => x.ProductId == item.ProductId);

                        if (oldItem is not null)
                        {
                            var newItem = new CartItem()
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
    }
}