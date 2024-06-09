namespace eShop.Domain.DTOs.Responses
{
    public class UpdateProductResponse
    {
        public Guid ProductId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
