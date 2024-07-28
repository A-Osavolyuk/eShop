namespace eShop.Domain.DTOs.Requests.Auth;

public record class LoginRequest : RequestBase
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
