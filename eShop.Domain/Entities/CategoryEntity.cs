using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore] public ICollection<SubcategoryEntity> Subcategories { get; set; } = null!;
    }
}
