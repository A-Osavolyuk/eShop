using eShop.Domain.Entities.AuthApi;

namespace eShop.Domain.Requests.AuthApi.Admin;

public record RemoveUserRoleRequest
{
    public Guid UserId { get; set; }
    public Role Role { get; set; } = null!;
}