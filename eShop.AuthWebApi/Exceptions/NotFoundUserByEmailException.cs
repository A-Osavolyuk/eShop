using eShop.Domain.Interfaces;

namespace eShop.AuthWebApi.Exceptions
{
    public class NotFoundUserByEmailException(string Email) : Exception($"Cannot find user with email: {Email}"), INotFoundException;
}
