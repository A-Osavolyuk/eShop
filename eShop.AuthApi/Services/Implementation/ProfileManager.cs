namespace eShop.AuthApi.Services.Implementation;

internal sealed class ProfileManager(AuthDbContext context) : IProfileManager
{
    private readonly AuthDbContext context = context;

    public async ValueTask<PersonalDataEntity?> FindPersonalDataAsync(AppUser user)
    {
        var data = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (data is null)
        {
            return null;
        }

        return data;
    }

    public async ValueTask<IdentityResult> SetPersonalDataAsync(AppUser user, PersonalDataEntity personalData)
    {
        personalData.UserId = user.Id;
        await context.PersonalData.AddAsync(personalData);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> ChangePersonalDataAsync(AppUser user, PersonalDataEntity personalData)
    {
        var data = await context.PersonalData.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (data is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Code = "404",
                Description = "User didn't set personal data yet"
            });
        }

        var newData = new PersonalDataEntity()
        {
            Id = data.Id,
            UserId = user.Id,
            Gender = personalData.Gender,
            FirstName = personalData.FirstName,
            LastName = personalData.LastName,
            DateOfBirth = personalData.DateOfBirth
        };

        context.PersonalData.Update(newData);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async ValueTask<IdentityResult> RemovePersonalDataAsync(AppUser user)
    {
        var data = await context.PersonalData
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (data is null)
        {
            return IdentityResult.Failed(new IdentityError() {Code = "404", Description = "Cannot find personal data"});
        }

        context.PersonalData.Remove(data);
        await context.SaveChangesAsync();

        return IdentityResult.Success;
    }
}