using eShop.Domain.Common.Api;
using eShop.Domain.Interfaces.Client;
using eShop.Domain.Requests.Api.Review;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class ReviewService(IHttpClientService httpClient, IConfiguration configuration) : IReviewService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async Task<Response> CreateReviewAsync(CreateReviewRequest request) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/create-review", Method: HttpMethods.Post, Data: request));

    public async Task<Response> DeleteReviewsWithProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/delete-reviews-with-product-id/{Id}", Method: HttpMethods.Delete));

    public async Task<Response> GetReviewListByProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/get-reviews-by-product-id/{Id}", Method: HttpMethods.Get));

    public async Task<Response> UpdateReviewAsync(UpdateReviewRequest request) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/update-review", Method: HttpMethods.Put, Data: request));
}