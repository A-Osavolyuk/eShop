﻿namespace eShop.Domain.Entities.ProductApi;

public class ClothingEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}