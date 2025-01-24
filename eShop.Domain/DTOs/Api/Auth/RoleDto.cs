namespace eShop.Domain.DTOs.Api.Auth;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name  { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public int MembersCount { get; set; }
}