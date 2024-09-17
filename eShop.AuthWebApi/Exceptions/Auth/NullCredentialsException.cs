namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NullCredentialsException() : Exception("Cannot get credentials from external login response."), IBadRequestException;
}
