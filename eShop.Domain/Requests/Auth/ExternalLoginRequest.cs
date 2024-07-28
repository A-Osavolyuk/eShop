using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Auth
{
    public record class ExternalLoginRequest : RequestBase
    {
        public string Provider { get; set; } = string.Empty;
        public string ReturnUri { get; set; } = string.Empty;
    }
}
