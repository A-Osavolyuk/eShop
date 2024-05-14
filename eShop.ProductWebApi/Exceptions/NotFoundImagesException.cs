using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundImagesException(string Message) : Exception(Message), INotFoundException;
}
