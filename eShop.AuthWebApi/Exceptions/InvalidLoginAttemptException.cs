namespace eShop.AuthWebApi.Exceptions;

public class InvalidLoginAttemptException() : Exception("Invalid login attempt. Check your login and password"), IBadRequestException;
