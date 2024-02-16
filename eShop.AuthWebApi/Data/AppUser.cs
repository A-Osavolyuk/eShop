using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Data
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
