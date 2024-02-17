namespace eShop.Domain.Exceptions
{
    public class NullRequestException(Type type) : Exception($"Request of type: {type} is null.");
}
