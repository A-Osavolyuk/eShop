using eShop.Domain.Types;
using UserModel = eShop.Domain.Models.Profile.UserModel;

namespace eShop.Domain.Interfaces.Client;

public interface IUserStorage
{
    public ValueTask<string> GetUserNameAsync();
    public ValueTask<string> GetUserIdAsync();
    public ValueTask<string> GetEmailAsync();
    public ValueTask<string> GetPhoneNumberAsync();
    public ValueTask<UserModel> GetUserAsync();
    public ValueTask<AccountData> GetAccountDataAsync();
    public ValueTask<PersonalData> GetPersonalDataAsync();
    
    public ValueTask SetUserAsync(UserModel user);
    public ValueTask SetAccountDataAsync(AccountData data);
    public ValueTask SetPersonalDataAsync(PersonalData data);
}