namespace eShop.Domain.Requests.Auth;

public record class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
