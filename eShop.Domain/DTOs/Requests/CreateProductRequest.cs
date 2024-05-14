using eShop.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public IFormFileCollection Images { get; set; } = new FormFileCollection();
        public Guid SupplierId { get; set; } = Guid.Empty;
        public Guid BrandId { get; set; } = Guid.Empty;
    }
}
