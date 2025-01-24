using eShop.Domain.Entities.AuthApi;

namespace eShop.Domain.Requests.AuthApi.Admin;

public record class RemoveUserRolesRequest
{
    public Guid UserId { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}