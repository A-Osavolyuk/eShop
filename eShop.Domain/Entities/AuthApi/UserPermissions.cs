﻿namespace eShop.Domain.Entities.AuthApi;

public class UserPermissions
{
    public string UserId { get; set; } = string.Empty;
    public Guid PermissionId { get; set; }

    public AppUser User { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}