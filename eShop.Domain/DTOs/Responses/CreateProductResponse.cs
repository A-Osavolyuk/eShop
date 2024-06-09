namespace eShop.Domain.DTOs.Responses
{
    public class CreateProductResponse
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; } = Guid.Empty;
        public Guid VariantId { get; set; } = Guid.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
