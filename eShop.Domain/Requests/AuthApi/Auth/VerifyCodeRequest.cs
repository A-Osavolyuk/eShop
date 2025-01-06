namespace eShop.Domain.Requests.AuthApi.Auth;

public class VerifyCodeRequest
{
    public string Code { get; set; } = string.Empty;
    public string SentTo { get; set; } = string.Empty;
    public VerificationCodeType CodeType { get; set; }
}