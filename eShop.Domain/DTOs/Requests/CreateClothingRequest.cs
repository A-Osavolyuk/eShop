using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateClothingRequest : CreateProductRequestBase
    {
        public CreateClothingRequest() => ProductType = ProductType.Clothing;
        public int Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}
