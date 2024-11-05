using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Comments;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class CommentService(IHttpClientService httpClient, IConfiguration configuration) : ICommentService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> GetCommentsAsync(Guid productId) => await httpClient.SendAsync(
            new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/get-comments/{productId}", Method: HttpMethods.GET));

        public async ValueTask<ResponseDTO> CreateCommentAsync(CreateCommentRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/create-comment",
                Data: request, Method: HttpMethods.POST));

        public async ValueTask<ResponseDTO> UpdateCommentAsync(UpdateCommentRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/update-comment", 
                Data: request, Method: HttpMethods.PUT));

        public async ValueTask<ResponseDTO> DeleteCommentAsync(DeleteCommentsRequest request) =>
            await httpClient.SendAsync(new RequestDto(
                Url: $"{configuration["Services:Gateway"]}/api/v1/Comments/delete-comment", 
                Data: request, Method: HttpMethods.DELETE));
    }
}