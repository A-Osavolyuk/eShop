namespace eShop.Domain.Entities.Api.Auth;

public class AppUser : IdentityUser
{
    public PersonalDataEntity? PersonalData { get; set; }
    public SecurityTokenEntity? AuthenticationToken { get; set; }
    public ICollection<UserPermissionsEntity> Permissions { get; set; } = null!;
}