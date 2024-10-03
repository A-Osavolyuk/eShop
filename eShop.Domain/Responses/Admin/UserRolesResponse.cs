using eShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Responses.Admin
{
    public record class UserRolesResponse
    {
        public Guid UserId { get; set; }
        public IList<RoleInfo> Roles { get; set; } = new List<RoleInfo>();
    }
}
