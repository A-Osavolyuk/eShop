﻿using eShop.Domain.DTOs.ProductApi;

namespace eShop.Domain.Requests.ProductApi.Product;

public record UpdateProductRequest()
{
    public Guid Id { get; set; }
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public BrandDto Brand { get; set; } = new BrandDto();
    public SellerDto Seller { get; set; } = new SellerDto();
    
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}