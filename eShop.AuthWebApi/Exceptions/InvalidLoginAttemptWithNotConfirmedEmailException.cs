namespace eShop.AuthWebApi.Exceptions
{
    public class InvalidLoginAttemptWithNotConfirmedEmailException() 
        : Exception("You cannot log in, your email is not confirmed."), IBadRequestException;
}
