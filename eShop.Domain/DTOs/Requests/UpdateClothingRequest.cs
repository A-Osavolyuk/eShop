using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateClothingRequest : UpdateProductRequestBase
    {
        public UpdateClothingRequest() => ProductType = ProductType.Clothing;
        public int Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}
