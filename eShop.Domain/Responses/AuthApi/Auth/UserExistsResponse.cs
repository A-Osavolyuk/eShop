namespace eShop.Domain.Responses.AuthApi.Auth;

public class UserExistsResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}