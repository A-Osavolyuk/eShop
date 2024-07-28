using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record RequestDto(
        string Url = "",
        object? Data = null!,
        HttpMethods Method = HttpMethods.GET);
}
