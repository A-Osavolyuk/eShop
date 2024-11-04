using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ClothingDTO : ProductDto
    {
        public ClothingDTO() => this.Category = Category.Clothing;
    }
}
