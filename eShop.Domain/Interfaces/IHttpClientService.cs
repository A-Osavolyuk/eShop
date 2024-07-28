using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDTO> SendAsync(RequestDto request, bool withBearer = true);
    }
}
