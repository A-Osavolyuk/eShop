using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class SubcategoryEntity
    {
        public Guid SubcategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        [JsonIgnore] public CategoryEntity Category { get; set; } = null!;
    }
}
