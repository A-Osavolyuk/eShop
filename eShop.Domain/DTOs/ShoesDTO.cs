using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ShoesDTO : ProductDto
    {
        public ShoesDTO() => this.Category = Category.Shoes;
    }
}
