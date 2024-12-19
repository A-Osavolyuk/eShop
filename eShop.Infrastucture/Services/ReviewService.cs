using eShop.Domain.Common.Api;
using eShop.Domain.Requests.ReviewApi.Review;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class ReviewService(IHttpClientService httpClient, IConfiguration configuration) : IReviewService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async Task<Response> CreateReviewAsync(CreateReviewRequest request) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/create-review", Method: HttpMethods.POST, Data: request));

    public async Task<Response> DeleteReviewsWithProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/delete-reviews-with-product-id/{Id}", Method: HttpMethods.DELETE));

    public async Task<Response> GetReviewListByProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/get-reviews-by-product-id/{Id}", Method: HttpMethods.GET));

    public async Task<Response> UpdateReviewAsync(UpdateReviewRequest request) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/update-review", Method: HttpMethods.PUT, Data: request));
}