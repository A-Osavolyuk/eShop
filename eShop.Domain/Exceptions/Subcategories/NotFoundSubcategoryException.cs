namespace eShop.Domain.Exceptions.Subcategories
{
    public class NotFoundSubcategoryException : Exception
    {
        public NotFoundSubcategoryException(Guid id) : base($"Cannot find subcategory with id: {id}.") { }
        public NotFoundSubcategoryException(string name) : base($"Cannot find subcategory with name: {name}.") { }
    }
}
