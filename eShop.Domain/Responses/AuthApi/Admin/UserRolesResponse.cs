using eShop.Domain.Entities.AuthApi;
using eShop.Domain.Types;

namespace eShop.Domain.Responses.AuthApi.Admin;

public record class UserRolesResponse
{
    public Guid UserId { get; set; }
    public IList<RoleInfo> Roles { get; set; } = new List<RoleInfo>();
}