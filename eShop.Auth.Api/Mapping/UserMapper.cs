using eShop.Auth.Api.Data.Entities;

namespace eShop.Auth.Api.Mapping;

public class UserMapper
{
    public static AppUser ToAppUser(RegistrationRequest request)
    {
        return new AppUser()
        {
            Email = request.Email,
            UserName = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            NormalizedUserName = request.Email.ToUpper(),
        };
    }

    public static AccountData ToAccountData(AppUser user)
    {
        return new AccountData()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!,
            EmailConfirmed = user.EmailConfirmed,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
        };
    }

    public static LockoutStatusResponse ToUserLockoutStatusResponse(LockoutStatus status)
    {
        return new LockoutStatusResponse()
        {
            LockoutEnd = status.LockoutEnd,
            LockoutEnabled = status.LockoutEnabled
        };
    }
}