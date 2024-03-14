namespace eShop.Domain.DTOs.Responses
{
    public class SubcategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
