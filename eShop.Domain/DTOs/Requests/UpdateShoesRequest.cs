using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateShoesRequest : UpdateProductRequestBase
    {
        public UpdateShoesRequest() => ProductType = ProductType.Shoes;
        public int Size { get; set; }
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
