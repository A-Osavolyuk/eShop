using eShop.Domain.Entities.Admin;
using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public PersonalData? PersonalData { get; set; }
    }
}
