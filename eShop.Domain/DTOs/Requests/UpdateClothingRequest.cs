using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateClothingRequest : UpdateProductRequestBase
    {
        public UpdateClothingRequest() => ProductType = Categoty.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
