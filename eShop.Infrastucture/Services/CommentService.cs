using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class CommentService(IHttpClientService httpClient, IConfiguration configuration) : ICommentService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDTO> GetCommentListWithReviewId(Guid Id) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:ReviewsWebAPi"]}/api/v1/Comments/get-comments-by-review-id/{Id}", Method: HttpMethods.GET));
    }
}
