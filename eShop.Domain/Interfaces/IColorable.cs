using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;
using eShop.Domain.Enums;

namespace eShop.Domain.Interfaces
{
    public interface IColorable
    {
        public List<ProductColor> Colors { get; set; }

        public IEnumerable<Product> CreateVariants();
    }
}
