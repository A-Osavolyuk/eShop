using eShop.Domain.Interfaces;

namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotFoundUserByEmailException(string Email) : Exception($"Cannot find user with email: {Email}"), INotFoundException;
}
