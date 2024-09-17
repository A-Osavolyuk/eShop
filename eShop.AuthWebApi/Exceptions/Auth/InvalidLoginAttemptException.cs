namespace eShop.AuthWebApi.Exceptions.Auth;

public class InvalidLoginAttemptException() : Exception("Invalid login attempt. Check your login and password"), IBadRequestException;
