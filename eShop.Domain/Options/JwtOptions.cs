namespace eShop.Domain.Options;

public record JwtOptions(
    int ExpirationSeconds,
    string Key = "",
    string Audience = "",
    string Issuer = ""
    );
