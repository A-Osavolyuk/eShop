namespace eShop.Infrastructure.Services;

public class ReviewService(IHttpClientService httpClient, IConfiguration configuration) : IReviewService
{
    private readonly IHttpClientService httpClient = httpClient;
    private readonly IConfiguration configuration = configuration;

    public async Task<Response> CreateReviewAsync(CreateReviewRequest request) => await httpClient.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/create-review",
            Methods: HttpMethods.Post, Data: request));

    public async Task<Response> DeleteReviewsWithProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url:
        $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/delete-reviews-with-product-id/{Id}",
        Methods: HttpMethods.Delete));

    public async Task<Response> GetReviewListByProductIdAsync(Guid Id) => await httpClient.SendAsync(new Request(
        Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/get-reviews-by-product-id/{Id}",
        Methods: HttpMethods.Get));

    public async Task<Response> UpdateReviewAsync(UpdateReviewRequest request) => await httpClient.SendAsync(
        new Request(
            Url: $"{configuration["Configuration:Services:Proxy:Gateway"]}/api/v1/Reviews/update-review",
            Methods: HttpMethods.Put, Data: request));
}