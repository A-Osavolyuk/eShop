using eShop.Domain.Entities.AuthApi;

namespace eShop.Domain.Responses.AuthApi.Admin;

public record class UserRolesResponse
{
    public Guid UserId { get; set; }
    public IList<RoleInfo> Roles { get; set; } = new List<RoleInfo>();
}