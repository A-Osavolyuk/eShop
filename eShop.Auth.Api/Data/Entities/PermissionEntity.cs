namespace eShop.Auth.Api.Data.Entities;

public class PermissionEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore] public ICollection<UserPermissionsEntity> Permissions { get; set; } = null!;
}