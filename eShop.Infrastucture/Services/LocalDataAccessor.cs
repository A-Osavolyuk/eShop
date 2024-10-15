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

        public async ValueTask<UserDataModel?> ReadUserDataAsync()
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
    }
}