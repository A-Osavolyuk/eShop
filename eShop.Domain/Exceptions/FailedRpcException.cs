namespace eShop.Domain.Exceptions;

public class FailedRpcException(string message) : Exception(message);