using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Subcategories
{
    public class NotFoundSubcategoryException : Exception, INotFoundException
    {
        public NotFoundSubcategoryException(Guid id) : base($"Cannot find subcategory with id: {id}.") { }
        public NotFoundSubcategoryException(string name) : base($"Cannot find subcategory with name: {name}.") { }
    }
}
