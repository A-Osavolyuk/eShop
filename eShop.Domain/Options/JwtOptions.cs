namespace eShop.Domain.Options;

public class JwtOptions
{
    public int ExpirationSeconds { get; set; }
    public string Key { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Issuer { get; set; } = "";
}
