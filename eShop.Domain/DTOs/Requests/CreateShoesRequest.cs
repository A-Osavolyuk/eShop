using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateShoesRequest : CreateProductRequestBase
    {
        public CreateShoesRequest() => ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
