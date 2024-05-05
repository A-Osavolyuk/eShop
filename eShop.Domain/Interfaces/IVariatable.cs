using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces
{
    public interface IVariable
    {
        public IEnumerable<Product> CreateVariants();
    }
}
