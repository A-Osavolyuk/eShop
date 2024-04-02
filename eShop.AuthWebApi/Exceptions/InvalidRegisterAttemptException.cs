namespace eShop.AuthWebApi.Exceptions
{
    public class InvalidRegisterAttemptException() : Exception("Invalid registration attempt."), IInternalServerError;
}
