namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class InvalidRegisterAttemptException() : Exception("Invalid registration attempt."), IInternalServerError;
}
