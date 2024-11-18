namespace eShop.Domain.Requests.Brand
{
    public record CreateBrandRequest : RequestBase
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
