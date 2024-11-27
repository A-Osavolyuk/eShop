using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services
{
    public class CommentService(IHttpClientService httpClient, IConfiguration configuration) : ICommentService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> GetCommentsAsync(Guid productId) => await httpClient.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/get-comments/{productId}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDto> CreateCommentAsync(CreateCommentRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/create-comment", Method: HttpMethods.POST, Data: request));

        public async ValueTask<ResponseDto> UpdateCommentAsync(UpdateCommentRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/update-comment", Method: HttpMethods.PUT, Data: request));

        public async ValueTask<ResponseDto> DeleteCommentAsync(DeleteCommentsRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/delete-comment", Method: HttpMethods.DELETE, Data: request));
    }
}