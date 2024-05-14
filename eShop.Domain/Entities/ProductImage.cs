namespace eShop.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid VariantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
    }
}
