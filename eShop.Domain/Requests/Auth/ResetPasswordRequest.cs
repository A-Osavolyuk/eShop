namespace eShop.Domain.Requests.Auth
{
    public record class ResetPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
