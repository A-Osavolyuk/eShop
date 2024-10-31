using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions;

public class BadRequestException(string message) : Exception(message), IBadRequestException;