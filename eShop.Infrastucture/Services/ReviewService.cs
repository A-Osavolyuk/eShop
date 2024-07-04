using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Requests.Review;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class ReviewService(IHttpClientService httpClient, IConfiguration configuration) : IReviewService
    {
        private readonly IHttpClientService httpClient = httpClient;
        private readonly IConfiguration configuration = configuration;

        public async Task<ResponseDTO> CreateReviewAsync(CreateReviewRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:ReviewsWebAPi"]}/api/v1/Reviews/create-review", Data: request, Method: HttpMethods.POST));

        public async Task<ResponseDTO> DeleteReviewsWithProductIdAsync(Guid Id) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:ReviewsWebAPi"]}/api/v1/Reviews/delete-reviews-with-product-id/{Id}", Method: HttpMethods.DELETE));

        public async Task<ResponseDTO> GetReviewListByProductIdAsync(Guid Id) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:ReviewsWebAPi"]}/api/v1/Reviews/get-reviews-by-product-id/{Id}", Method: HttpMethods.GET));

        public async Task<ResponseDTO> UpdateReviewAsync(UpdateReviewRequest request) => await httpClient.SendAsync(new RequestDto(
            Url: $"{configuration["Services:ReviewsWebAPi"]}/api/v1/Reviews/update-review", Data: request, Method: HttpMethods.PUT));
    }
}
