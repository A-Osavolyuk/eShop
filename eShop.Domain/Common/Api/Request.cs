namespace eShop.Domain.Common.Api;

public record Request(
    string Url,
    HttpMethods Method,
    object? Data = null!);