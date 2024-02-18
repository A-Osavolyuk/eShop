using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDto> SendAsync(RequestDto request, bool withBearer = false);
    }
}
