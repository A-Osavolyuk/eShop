using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record RequestDto(
        string Url,
        HttpMethods Method,
        object? Data = default!);
}
