using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
