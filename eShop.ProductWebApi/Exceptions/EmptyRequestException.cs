using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class EmptyRequestException() : Exception("Cannot continue operation due to empty request"), IBadRequestException;
}
