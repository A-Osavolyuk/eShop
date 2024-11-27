namespace eShop.Domain.Interfaces
{
    public interface IHttpClientService
    {
        public ValueTask<ResponseDto> SendAsync(RequestDto request, bool withBearer = true);
        public ValueTask<ResponseDto> SendFilesAsync(FileRequestDto request, bool withBearer = true);
    }
}
