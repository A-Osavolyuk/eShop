using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateClothingRequest : CreateProductRequestBase
    {
        public CreateClothingRequest() => ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
