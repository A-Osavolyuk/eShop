﻿using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ClothingDTO : ProductDTO
    {
        public ClothingDTO() => this.ProductType = ProductType.Clothing;
        public int Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}