﻿using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Clothing : Product
    {
        public Clothing() : base() => Category = Category.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
