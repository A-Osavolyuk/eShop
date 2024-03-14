namespace eShop.Domain.DTOs.Responses
{
    public class SubcategoryDto
    {
        public string SubcategoryName { get; set; } = string.Empty;
        public CategoryDto Category { get; set; } = null!;
    }
}
