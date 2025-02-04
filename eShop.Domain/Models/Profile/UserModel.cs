using eShop.Domain.Types;

namespace eShop.Domain.Models.Profile;

public class UserModel
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalData PersonalData { get; set; } = null!;
    public PermissionsData PermissionsData { get; set; } = null!;
}