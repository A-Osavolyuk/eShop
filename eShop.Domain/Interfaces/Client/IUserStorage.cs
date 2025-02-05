using eShop.Domain.Types;
using UserModel = eShop.Domain.Models.UserModel;

namespace eShop.Domain.Interfaces.Client;

public interface IUserStorage
{
    public ValueTask<string?> GetUserNameAsync();
    public ValueTask<string?> GetUserIdAsync();
    public ValueTask<string?> GetEmailAsync();
    public ValueTask<string?> GetPhoneNumberAsync();
    public ValueTask<UserModel?> GetUserAsync();
    public ValueTask<AccountData?> GetAccountDataAsync();
    public ValueTask<PersonalData?> GetPersonalDataAsync();
    public ValueTask<PermissionsData?> GetPermissionDataAsync();
    public ValueTask<SecurityData?> GetSecurityDataAsync();
    
    public ValueTask SetUserAsync(UserModel model);
    public ValueTask SetAccountDataAsync(AccountData data);
    public ValueTask SetPersonalDataAsync(PersonalData data);
    public ValueTask SetPermissionDataAsync(PermissionsData data);
    public ValueTask SetSecurityDataAsync(SecurityData data);
}