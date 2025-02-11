using eShop.Domain.Types;

namespace eShop.Domain.Responses.Api.Auth;

public class TwoFactorAuthenticationStateResponse
{
    public TwoFactorAuthenticationState State { get; set; }
}