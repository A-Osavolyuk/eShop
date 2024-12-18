using eShop.Domain.Common.Api;
using eShop.Domain.Requests.ReviewApi.Review;

namespace eShop.Domain.Interfaces;

public interface IReviewService
{
    public Task<Response> GetReviewListByProductIdAsync(Guid Id);
    public Task<Response> CreateReviewAsync(CreateReviewRequest request);
    public Task<Response> DeleteReviewsWithProductIdAsync(Guid Id);
    public Task<Response> UpdateReviewAsync(UpdateReviewRequest request);
}