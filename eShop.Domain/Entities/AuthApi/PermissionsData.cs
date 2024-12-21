namespace eShop.Domain.Entities.AuthApi;

public class PermissionsData
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public List<RoleInfo> Roles { get; set; } = new List<RoleInfo>();
    public List<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
}