﻿namespace eShop.Domain.Types;

public class RoleData : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
}