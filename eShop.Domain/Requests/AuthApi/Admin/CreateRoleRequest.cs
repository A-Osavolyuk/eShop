namespace eShop.Domain.Requests.AuthApi.Admin;

public record CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;
}