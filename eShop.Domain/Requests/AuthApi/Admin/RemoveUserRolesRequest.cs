using eShop.Domain.Entities.AuthApi;

namespace eShop.Domain.Requests.AuthApi.Admin;

public record class RemoveUserRolesRequest
{
    public Guid UserId { get; set; }
    public List<Role> Roles { get; set; } = new List<Role>();
}