﻿using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;

namespace eShop.Domain.Requests.Product
{
    public record UpdateProductRequest : RequestBase
    {
        public Guid Id { get; set; }
        public Guid VariantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid BrandId { get; set; } = Guid.Empty;
        public HashSet<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
