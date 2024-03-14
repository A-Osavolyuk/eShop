namespace eShop.Domain.DTOs.Requests
{
    public class CreateUpdateSubcategoryRequestDto
    {
        public string SubcategoryName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
