using eShop.Domain.Entities.Admin;
using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Entities.Auth
{
    public class AppUser : IdentityUser
    {
        public PersonalData? PersonalData { get; set; }
        public UserAuthenticationToken? AuthenticationToken { get; set; }
        public ICollection<UserPermissions> Permissions { get; set; } = null!;
    }
}
