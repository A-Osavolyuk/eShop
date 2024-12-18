namespace eShop.Domain.Entities.AuthApi;

public class AppUser : IdentityUser
{
    public PersonalDataEntity? PersonalData { get; set; }
    public UserAuthenticationToken? AuthenticationToken { get; set; }
    public ICollection<UserPermissions> Permissions { get; set; } = null!;
}