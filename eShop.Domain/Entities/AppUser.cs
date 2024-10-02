using eShop.Domain.DTOs.Requests.Auth;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public PersonalData? PersonalData { get; set; }
    }
}
