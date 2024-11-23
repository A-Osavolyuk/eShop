using eShop.Domain.Entities.Auth;
using eShop.Domain.Responses.Admin;

namespace eShop.Application.Mapping;

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
    
    public static AppUser ToAppUser(AccountData data)
    {
        return new AppUser()
        {
            Id = data.Id.ToString(),
            Email = data.Email,
            UserName = data.UserName,
            NormalizedEmail = data.Email.ToUpper(),
            NormalizedUserName = data.UserName.ToUpper(),
            PhoneNumber = data.PhoneNumber,
            PhoneNumberConfirmed = data.PhoneNumberConfirmed,
            EmailConfirmed = data.EmailConfirmed,
            LockoutEnabled = data.LockoutEnabled,
            LockoutEnd = data.LockoutEnd
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