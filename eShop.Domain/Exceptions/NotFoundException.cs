using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message), INotFoundException;