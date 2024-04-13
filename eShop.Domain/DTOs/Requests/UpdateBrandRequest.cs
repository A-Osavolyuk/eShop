namespace eShop.Domain.DTOs.Requests
{
    public class UpdateBrandRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
