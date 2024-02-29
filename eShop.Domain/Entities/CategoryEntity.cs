namespace eShop.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SubcategoryEntity> Subcategories { get; set; } = null!;
    }
}
