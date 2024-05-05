using eShop.Domain.Enums;

namespace eShop.Domain.Interfaces
{
    public interface IColorable
    {
        public List<ProductColor> Colors { get; set; }
    }
}
