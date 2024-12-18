namespace eShop.Domain.Entities.AuthApi;

public class UserData
{
    public AccountData AccountData { get; set; } = null!;
    public PersonalDataEntity PersonalDataEntity { get; set; } = null!;
    public PermissionsData PermissionsData { get; set; } = null!;
}