﻿namespace eShop.Domain.DTOs;

public class BrandDto : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}