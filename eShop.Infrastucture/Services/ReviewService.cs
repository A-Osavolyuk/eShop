using eShop.Domain.DTOs;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.Domain.Requests.Review;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class ReviewService(IHttpClientService httpClient, IConfiguration configuration) : IReviewService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async Task<ResponseDto> CreateReviewAsync(CreateReviewRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Reviews/create-review", Method: HttpMethods.POST, Data: request));

        public async Task<ResponseDto> DeleteReviewsWithProductIdAsync(Guid Id) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Reviews/delete-reviews-with-product-id/{Id}", Method: HttpMethods.DELETE));

        public async Task<ResponseDto> GetReviewListByProductIdAsync(Guid Id) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Reviews/get-reviews-by-product-id/{Id}", Method: HttpMethods.GET));

        public async Task<ResponseDto> UpdateReviewAsync(UpdateReviewRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:Gateway"]}/api/v1/Reviews/update-review", Method: HttpMethods.PUT, Data: request));
    }
}
