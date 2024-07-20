using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions
{
    public class NullRequestException() : Exception("Request cannot be null"), IBadRequestException;
}
