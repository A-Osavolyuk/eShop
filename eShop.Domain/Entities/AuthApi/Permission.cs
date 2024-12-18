namespace eShop.Domain.Entities.AuthApi;

public class Permission
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<UserPermissions> Permissions { get; set; } = null!;
}