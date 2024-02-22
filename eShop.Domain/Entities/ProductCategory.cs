using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class ProductCategory
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
