using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateShoesRequest : UpdateProductRequestBase
    {
        public UpdateShoesRequest() => ProductType = ProductType.Shoes;
        public int Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}
