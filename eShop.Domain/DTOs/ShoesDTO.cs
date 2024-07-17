using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ShoesDTO : ProductDTO
    {
        public ShoesDTO() => this.Category = Category.Shoes;
    }
}
