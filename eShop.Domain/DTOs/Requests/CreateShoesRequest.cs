using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateShoesRequest : CreateProductRequestBase
    {
        public CreateShoesRequest() => ProductType = ProductType.Shoes;
        public int Size { get; set; }
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
