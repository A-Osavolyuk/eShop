using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Common
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
