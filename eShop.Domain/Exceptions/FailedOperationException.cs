using eShop.Domain.Exceptions.Interfaces;

namespace eShop.Domain.Exceptions;

public class FailedOperationException(string message) : Exception(message), IInternalServerError;