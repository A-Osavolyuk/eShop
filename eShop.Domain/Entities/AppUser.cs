using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
