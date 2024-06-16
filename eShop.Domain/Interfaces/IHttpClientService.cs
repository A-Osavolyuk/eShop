using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDTO> SendAsync(RequestDto request, bool withBearer = true);
    }
}
