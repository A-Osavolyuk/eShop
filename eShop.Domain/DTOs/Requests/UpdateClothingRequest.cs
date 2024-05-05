using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateClothingRequest : ProductRequestBase
    {
        public UpdateClothingRequest() => ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
