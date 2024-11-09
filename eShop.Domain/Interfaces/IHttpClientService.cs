using eShop.Domain.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDto> SendAsync(RequestDto request, bool withBearer = true);
        public ValueTask<ResponseDto> SendFilesAsync(FileRequestDto request, bool withBearer = true);
    }
}
