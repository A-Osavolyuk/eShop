using Blazored.LocalStorage;
using eShop.Domain.DTOs;
using eShop.Domain.Interfaces;

namespace eShop.Infrastructure.Services
{
    public class LocalDataAccessor(ILocalStorageService localStorageService) : ILocalDataAccessor
    {
        private readonly ILocalStorageService localStorageService = localStorageService;

        private async ValueTask RemovePersonalDataAsync()
        {
            await localStorageService.RemoveItemAsync("personal-data");
        }

        private async ValueTask RemovePhoneNumberAsync()
        {
            await localStorageService.RemoveItemAsync("phone-number");
        }

        private async ValueTask RemoveEmailAsync()
        {
            await localStorageService.RemoveItemAsync("email");
        }

        private async ValueTask RemoveUserNameAsync()
        {
            await localStorageService.RemoveItemAsync("username");
        }

        private async ValueTask RemoveUserIdAsync()
        {
            await localStorageService.RemoveItemAsync("user-id");
        }

        public async ValueTask<PersonalDataDTO> GetPersonalDataAsync()
        {
            var phoneNumber = await localStorageService.GetItemAsync<PersonalDataDTO>("personal-data");
            return phoneNumber!;
        }

        public async ValueTask<string> GetPhoneNumberAsync()
        {
            var phoneNumber = await localStorageService.GetItemAsStringAsync("phone-number");
            return phoneNumber!;
        }

        public async ValueTask SetPersonalDataAsync(PersonalDataDTO PersonalData)
        {
            if (PersonalData is not null)
                await localStorageService.SetItemAsync("personal-data", PersonalData);
        }

        public async ValueTask SetPhoneNumberAsync(string PhoneNumber)
        {
            if (!string.IsNullOrEmpty(PhoneNumber))
                await localStorageService.SetItemAsStringAsync("phone-number", PhoneNumber);
        }

        public async ValueTask RemoveDataAsync()
        {
            await RemovePersonalDataAsync();
            await RemovePhoneNumberAsync();
            await RemoveEmailAsync();
            await RemoveUserNameAsync();
            await RemoveUserIdAsync();
        }

        public async ValueTask SetEmailAsync(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
                await localStorageService.SetItemAsStringAsync("email", Email);
        }

        public async ValueTask<string> GetEmailAsync()
        {
            var email = await localStorageService.GetItemAsStringAsync("email");
            return email!;
        }

        public async ValueTask SetUserNameAsync(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
                await localStorageService.SetItemAsStringAsync("username", UserName);
        }

        public async ValueTask<string> GetUserNameAsync()
        {
            var username = await localStorageService.GetItemAsStringAsync("username");
            return username!;
        }

        public async ValueTask SetUserIdAsync(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
                await localStorageService.SetItemAsStringAsync("user-id", UserId);
        }

        public async ValueTask<string> GetUserIdAsync()
        {
            var userId = await localStorageService.GetItemAsStringAsync("user-id");
            return userId!;
        }
    }
}