using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<Response> SendAsync(RequestDto request, bool withBearer = true);
        public ValueTask<Response> SendFilesAsync(FileRequestDto request, bool withBearer = true);
    }
}
