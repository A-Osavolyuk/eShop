namespace eShop.AuthApi.Services.Interfaces;

public interface IProfileManager
{
    public ValueTask<PersonalDataEntity?> FindPersonalDataAsync(AppUser user);
    public ValueTask<IdentityResult> SetPersonalDataAsync(AppUser user, PersonalDataEntity personalData);
    public ValueTask<IdentityResult> ChangePersonalDataAsync(AppUser user, PersonalDataEntity personalData);
    public ValueTask<IdentityResult> RemovePersonalDataAsync(AppUser user);
}