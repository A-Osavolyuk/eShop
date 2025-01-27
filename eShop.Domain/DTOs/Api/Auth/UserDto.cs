using eShop.Domain.Types;

namespace eShop.Domain.DTOs.Api.Auth;

public class UserDto
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalData PersonalData { get; set; } = null!;
    public PermissionsData PermissionsData { get; set; } = null!;
}