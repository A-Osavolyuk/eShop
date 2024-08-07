using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface ILocalDataAccessor
    {
        public ValueTask RemoveDataAsync();
        public ValueTask SetPersonalDataAsync(PersonalDataDTO PersonalData);
        public ValueTask<PersonalDataDTO> GetPersonalDataAsync();
        public ValueTask SetPhoneNumberAsync(string PhoneNumber);
        public ValueTask<string> GetPhoneNumberAsync();
        public ValueTask SetEmailAsync(string Email);
        public ValueTask<string> GetEmailAsync();
        public ValueTask SetUserNameAsync(string UserName);
        public ValueTask<string> GetUserNameAsync();
        public ValueTask SetUserIdAsync(string UserId);
        public ValueTask<string> GetUserIdAsync();
        public ValueTask SetCartAsync(CartDTO Cart);
        public ValueTask<CartDTO> GetCartAsync();
    }
}
