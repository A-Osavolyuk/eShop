namespace eShop.Domain.Requests.AuthApi.Admin;

public record DeleteRoleRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}