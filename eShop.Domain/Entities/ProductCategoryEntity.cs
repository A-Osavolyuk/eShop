namespace eShop.Domain.Entities
{
    public class ProductCategoryEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
