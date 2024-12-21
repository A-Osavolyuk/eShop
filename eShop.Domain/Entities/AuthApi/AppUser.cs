namespace eShop.Domain.Entities.AuthApi;

public class AppUser : IdentityUser
{
    public PersonalDataEntity? PersonalData { get; set; }
    public UserAuthenticationTokenEntity? AuthenticationToken { get; set; }
    public ICollection<UserPermissionsEntity> Permissions { get; set; } = null!;
}