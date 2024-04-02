namespace eShop.AuthWebApi.Exceptions
{
    public class NotFoundUserByIdException(string Id) : Exception($"Cannot find user with id: {Id}"), INotFoundException;
}
