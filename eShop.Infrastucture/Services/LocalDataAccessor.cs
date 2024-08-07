using Blazored.LocalStorage;
using eShop.Domain.DTOs;
using eShop.Domain.Interfaces;

namespace eShop.Infrastructure.Services
{
    public class LocalDataAccessor(ILocalStorageService localStorageService) : ILocalDataAccessor
    {
        private readonly ILocalStorageService localStorageService = localStorageService;

        private async ValueTask RemovePersonalDataAsync() => await localStorageService.RemoveItemAsync("personal-data");

        private async ValueTask RemovePhoneNumberAsync() => await localStorageService.RemoveItemAsync("phone-number");

        private async ValueTask RemoveEmailAsync() => await localStorageService.RemoveItemAsync("email");

        private async ValueTask RemoveUserNameAsync() => await localStorageService.RemoveItemAsync("username");

        private async ValueTask RemoveUserIdAsync() => await localStorageService.RemoveItemAsync("user-id");

        public async ValueTask<PersonalDataDTO> GetPersonalDataAsync() => (await localStorageService.GetItemAsync<PersonalDataDTO>("personal-data"))!;

        public async ValueTask<string> GetPhoneNumberAsync() => (await localStorageService.GetItemAsStringAsync("phone-number"))!;

        public async ValueTask SetPersonalDataAsync(PersonalDataDTO PersonalData) => await localStorageService.SetItemAsync("personal-data", PersonalData);

        public async ValueTask SetPhoneNumberAsync(string PhoneNumber) => await localStorageService.SetItemAsStringAsync("phone-number", PhoneNumber);

        public async ValueTask RemoveDataAsync() => await localStorageService.ClearAsync();

        public async ValueTask SetEmailAsync(string Email) => await localStorageService.SetItemAsStringAsync("email", Email);

        public async ValueTask<string> GetEmailAsync() => (await localStorageService.GetItemAsStringAsync("email"))!;

        public async ValueTask SetUserNameAsync(string UserName) => await localStorageService.SetItemAsStringAsync("username", UserName);

        public async ValueTask<string> GetUserNameAsync() => (await localStorageService.GetItemAsStringAsync("username"))!;

        public async ValueTask SetUserIdAsync(string UserId) => await localStorageService.SetItemAsStringAsync("user-id", UserId);

        public async ValueTask<string> GetUserIdAsync() => (await localStorageService.GetItemAsStringAsync("user-id"))!;

        public async ValueTask SetCartAsync(CartDTO Cart) => await localStorageService.SetItemAsync("cart", Cart);

        public async ValueTask<CartDTO> GetCartAsync() => (await localStorageService.GetItemAsync<CartDTO>("cart"))!;
    }
}