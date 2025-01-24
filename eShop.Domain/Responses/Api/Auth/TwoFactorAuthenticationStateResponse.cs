using eShop.Domain.Common.Security;

namespace eShop.Domain.Responses.Api.Auth;

public class TwoFactorAuthenticationStateResponse
{
    public TwoFactorAuthenticationState State { get; set; }
}