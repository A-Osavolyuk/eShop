namespace eShop.ProductWebApi.Exceptions
{
    public class NotCreatedProductException() : Exception($"Cannot create product due to server error.");
}
