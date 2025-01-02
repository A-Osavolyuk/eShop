using eShop.Domain.Common.Security;

namespace eShop.Domain.Responses.AuthApi.Auth;

public class TwoFactorAuthenticationStateResponse
{
    public TwoFactorAuthenticationState State { get; set; }
}