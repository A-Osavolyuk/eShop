namespace eShop.Domain.DTOs.Requests
{
    public class CreateUpdateSubcategoryRequest
    {
        public string SubcategoryName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
