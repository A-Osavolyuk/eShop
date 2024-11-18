using Microsoft.AspNetCore.Authentication;

namespace eShop.Domain.Responses.Auth
{
    public class ExternalLoginResponse
    {
        public AuthenticationProperties AuthenticationProperties { get; set; } = null!;
        public string Provider { get; set; } = string.Empty;
    }
}
