using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ClothingDTO : ProductDTO
    {
        public ClothingDTO() => this.Category = Category.Clothing;
    }
}
