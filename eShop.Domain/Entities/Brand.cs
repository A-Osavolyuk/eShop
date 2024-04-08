namespace eShop.Domain.Entities
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
