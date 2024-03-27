namespace eShop.AuthWebApi.Exceptions
{
    public class NotConfirmedEmailException() : Exception("Cannot confirm your email address due to incorrect token or server error.");
}
