﻿using UserModel = eShop.Domain.Models.UserModel;

namespace eShop.Domain.Interfaces.Client;

public interface IUserStorage
{
    public ValueTask<string?> GetUserNameAsync();
    public ValueTask<Guid> GetUserIdAsync();
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
    public ValueTask SetUserNameAsync(string userName);
    public ValueTask SetEmailAsync(string email);
    public ValueTask SetPhoneNumberAsync(string phoneNumber);
    
    public ValueTask ClearAsync();
}