namespace eShop.Domain.DTOs.Requests
{
    public class CreateSupplierRequest
    {
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
    }
}
