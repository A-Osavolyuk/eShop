using Blazored.LocalStorage;
using eShop.Domain.DTOs;
using eShop.Domain.Interfaces;
using eShop.Domain.Models;

namespace eShop.Infrastructure.Services
{
    public class LocalDataAccessor(ILocalStorageService localStorageService) : ILocalDataAccessor
    {
        private readonly ILocalStorageService localStorageService = localStorageService;

        public async ValueTask RemoveDataAsync() => await localStorageService.ClearAsync();

        public async ValueTask SetCartAsync(CartDTO Cart) => await localStorageService.SetItemAsync("cart", Cart);

        public async ValueTask<CartDTO> GetCartAsync() => (await localStorageService.GetItemAsync<CartDTO>("cart"))!;

        public async ValueTask WriteUserDataAsync(UserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await localStorageService.SetItemAsync("user", user);
        }

        public async ValueTask<UserModel> ReadUserDataAsync()
        {
            var user = await localStorageService.GetItemAsync<UserModel>("user");

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user;
        }
    }
}