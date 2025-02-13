namespace eShop.Domain.Models;

public record UserModel
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalData PersonalData { get; set; } = null!;
    public PermissionsData PermissionsData { get; set; } = null!;
    public SecurityData SecurityData { get; set; } = null!;
}