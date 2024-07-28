using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Domain.Requests.Admin
{
    public class RemoveUserRolesRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
