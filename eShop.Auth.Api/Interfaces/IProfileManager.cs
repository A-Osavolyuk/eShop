using eShop.Auth.Api.Data.Entities;

namespace eShop.Auth.Api.Services.Interfaces;

public interface IProfileManager
{
    public ValueTask<PersonalData?> FindPersonalDataAsync(AppUser user);
    public ValueTask<IdentityResult> SetPersonalDataAsync(AppUser user, PersonalDataEntity personalData);
    public ValueTask<IdentityResult> ChangePersonalDataAsync(AppUser user, PersonalDataEntity personalData);
    public ValueTask<IdentityResult> RemovePersonalDataAsync(AppUser user);
}