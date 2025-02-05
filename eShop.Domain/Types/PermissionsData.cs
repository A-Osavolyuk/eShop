﻿namespace eShop.Domain.Types;

public record PermissionsData
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public List<RoleData> Roles { get; set; } = new List<RoleData>();
    public List<Permission> Permissions { get; set; } = new List<Permission>();
}