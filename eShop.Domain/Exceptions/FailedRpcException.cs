namespace eShop.Domain.Exceptions
{
    public class FailedRpcException(string Message) : Exception(Message);
}
