namespace eShop.AuthWebApi.Exceptions
{
    public class WrongPasswordException() : Exception("Wrong password. Please check your password."), IBadRequestException;
}
