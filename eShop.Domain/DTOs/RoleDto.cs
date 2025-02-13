﻿namespace eShop.Domain.DTOs;

public class RoleDto : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name  { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public int MembersCount { get; set; }
}