using UserModel = eShop.Domain.Models.Profile.UserModel;

namespace eShop.Infrastructure.Storage;

public class UserStorage(ILocalStorageService localStorage) : IUserStorage
{
    private readonly ILocalStorageService localStorage = localStorage;

    private const string UserKey = "user";
    
    public async ValueTask<string?> GetUserNameAsync()
    {
        var model = await GetModelAsync();

        return model?.AccountData.UserName;
    }

    public async ValueTask<string?> GetUserIdAsync()
    {
        var model = await GetModelAsync();

        return model?.AccountData.Id.ToString();
    }

    public async ValueTask<string?> GetEmailAsync()
    {
        var model = await GetModelAsync();

        return model?.AccountData.Email;
    }

    public async ValueTask<string?> GetPhoneNumberAsync()
    {
        var model = await GetModelAsync();

        return model?.AccountData.PhoneNumber;
    }

    public async ValueTask<UserModel?> GetUserAsync()
    {
        var model = await GetModelAsync();

        return model;
    }

    public async ValueTask<AccountData?> GetAccountDataAsync()
    {
        var model = await GetModelAsync();

        return model?.AccountData;
    }

    public async ValueTask<PersonalData?> GetPersonalDataAsync()
    {
        var model = await GetModelAsync();

        return model?.PersonalData;
    }

    public async ValueTask SetUserAsync(UserModel model)
    {
        await SetModelAsync(model);
    }

    public async ValueTask SetAccountDataAsync(AccountData data)
    {
        var model = await GetModelAsync();
        var newModel = model! with { AccountData = data };
        await SetModelAsync(newModel);
    }

    public async ValueTask SetPersonalDataAsync(PersonalData data)
    {
        var model = await GetModelAsync();
        var newModel = model! with { PersonalData = data };
        await SetModelAsync(newModel);
    }

    private async ValueTask<UserModel?> GetModelAsync() => await localStorage.GetItemAsync<UserModel>(UserKey);
    private async ValueTask SetModelAsync(UserModel model) => await localStorage.SetItemAsync(UserKey, model);
}