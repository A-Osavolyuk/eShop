﻿using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Responses
{
    public class CategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<SubcategoryDto> Subcategories { get; set; } = null!;
    }
}