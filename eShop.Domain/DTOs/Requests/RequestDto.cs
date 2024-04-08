using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public record RequestDto(
        string Url = "", 
        object? Data = null!, 
        ApiMethod Method = ApiMethod.GET);
}
