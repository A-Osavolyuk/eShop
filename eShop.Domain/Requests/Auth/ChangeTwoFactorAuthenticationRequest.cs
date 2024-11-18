namespace eShop.Domain.Requests.Auth
{
    public record class ChangeTwoFactorAuthenticationRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
    }
}
