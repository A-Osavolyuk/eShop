namespace eShop.Domain.Exceptions.Products
{
    public class NotFoundProductException : Exception
    {
        public NotFoundProductException(Guid id) : base($"Cannot find Product with id: {id}.") { }
        public NotFoundProductException(string name) : base($"Cannot find Product with name: {name}.") { }
    }
}
