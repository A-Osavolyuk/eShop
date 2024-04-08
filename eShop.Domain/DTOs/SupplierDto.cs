namespace eShop.Domain.DTOs
{
    public class SupplierDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
