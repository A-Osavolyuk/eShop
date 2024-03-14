namespace eShop.Domain.DTOs.Requests
{
    public class CreateUpdateSupplierRequestDto
    {
        public string SupplierName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
