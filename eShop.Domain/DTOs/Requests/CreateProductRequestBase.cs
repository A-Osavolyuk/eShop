﻿using eShop.Domain.Entities;
using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateProductRequestBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public Money Price { get; set; } = null!;
        public Guid SupplierId { get; set; } = Guid.Empty;
        public Guid BrandId { get; set; } = Guid.Empty;
    }
}