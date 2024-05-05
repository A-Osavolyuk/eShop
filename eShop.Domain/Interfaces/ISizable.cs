using eShop.Domain.Enums;

namespace eShop.Domain.Interfaces
{
    public interface ISizeable
    {
        public List<ProductSize> Sizes { get; set; }
    }
}
