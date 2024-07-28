using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Auth
{
    public record class ResetPasswordRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
    }
}
