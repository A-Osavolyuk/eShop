namespace eShop.Domain.DTOs.Requests
{
    public class CreateUpdateSupplierRequest
    {
        public string SupplierName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
