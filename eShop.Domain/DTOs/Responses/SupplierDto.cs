namespace eShop.Domain.DTOs.Responses
{
    public class SupplierDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
