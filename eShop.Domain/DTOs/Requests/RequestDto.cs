using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public record RequestDto(
        string AccessToken = "", 
        string Url = "", 
        object? Data = null!, 
        ApiMethod Method = ApiMethod.GET);
}
