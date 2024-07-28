namespace eShop.Domain.Responses.Product
{
    public class UpdateProductResponse
    {
        public Guid ProductId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
