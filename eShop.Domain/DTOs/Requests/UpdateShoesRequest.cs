using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateShoesRequest : ProductRequestBase
    {
        public UpdateShoesRequest() => ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
