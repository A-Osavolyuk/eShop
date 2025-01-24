using eShop.Domain.Entities.Api.Auth;

namespace eShop.Domain.Types;

public class PermissionsData
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public List<RoleData> Roles { get; set; } = new List<RoleData>();
    public List<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
}