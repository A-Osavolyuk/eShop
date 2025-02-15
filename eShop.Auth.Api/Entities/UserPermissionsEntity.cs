﻿namespace eShop.Auth.Api.Entities;

public class UserPermissionsEntity
{
    public string UserId { get; set; } = string.Empty;
    public Guid PermissionId { get; set; }

    public AppUser User { get; set; } = null!;
    public PermissionEntity PermissionEntity { get; set; } = null!;
}