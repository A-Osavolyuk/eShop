using eShop.Domain.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Models
{
    public record CreateProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public IReadOnlyList<IBrowserFile> Files { get; set; } = [];
        public Guid BrandId { get; set; } = Guid.Empty;
        public IEnumerable<ProductSize> Sizes { get; set; } = Enumerable.Empty<ProductSize>();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
