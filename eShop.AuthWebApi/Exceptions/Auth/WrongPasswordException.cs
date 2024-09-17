namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class WrongPasswordException() : Exception("Wrong password. Please check your password."), IBadRequestException;
}
