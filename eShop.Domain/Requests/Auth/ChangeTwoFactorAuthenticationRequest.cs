namespace eShop.Domain.Requests.Auth
{
    public record class ChangeTwoFactorAuthenticationRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
