﻿namespace eShop.Domain.DTOs.ProductApi;

public record ClothingDto() : ProductDto
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}