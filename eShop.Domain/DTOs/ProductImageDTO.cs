namespace eShop.Domain.DTOs
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
    }
}
