namespace eShop.Domain.Exceptions
{
    public class NotFoundCategoryException : Exception
    {
        public NotFoundCategoryException(Guid id) : base($"Cannot find category with id: {id}.") { }
        public NotFoundCategoryException(string name) : base($"Cannot find category with name: {name}.") { }
    }
}
