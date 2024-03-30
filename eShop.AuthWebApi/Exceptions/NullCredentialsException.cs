namespace eShop.AuthWebApi.Exceptions
{
    public class NullCredentialsException() : Exception("Cannot get credentials from external login response.");
}
