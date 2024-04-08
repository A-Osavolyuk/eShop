namespace eShop.Domain.DTOs
{
    public class SubcategoryDto
    {
        public Guid SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = string.Empty;
        public CategoryDto Category { get; set; } = null!;
    }
}
