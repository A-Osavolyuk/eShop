namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class InvalidLoginAttemptWithNotConfirmedEmailException()
        : Exception("You cannot log in, your email is not confirmed."), IBadRequestException;
}
