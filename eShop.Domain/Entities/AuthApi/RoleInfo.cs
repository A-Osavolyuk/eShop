﻿namespace eShop.Domain.Entities.AuthApi;

public class RoleInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
}