namespace eShop.Domain.DTOs.Requests
{
    public class SubcategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
