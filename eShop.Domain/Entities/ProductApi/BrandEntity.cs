﻿namespace eShop.Domain.Entities.ProductApi;

public class BrandEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}