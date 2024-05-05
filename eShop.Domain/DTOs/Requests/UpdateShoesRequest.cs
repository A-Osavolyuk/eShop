using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateShoesRequest : ProductRequestBase, IVariable, ISizeable 
    {
        public UpdateShoesRequest() => ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
        public Guid VariantId { get; set; }
    }
}
