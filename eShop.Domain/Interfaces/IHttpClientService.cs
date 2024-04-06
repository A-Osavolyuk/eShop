using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDto> SendAsync(RequestDto request, bool withBearer = false);
    }
}
