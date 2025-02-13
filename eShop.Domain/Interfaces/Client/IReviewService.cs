namespace eShop.Domain.Interfaces.Client;

public interface IReviewService
{
    public Task<Response> GetReviewListByProductIdAsync(Guid id);
    public Task<Response> CreateReviewAsync(CreateReviewRequest request);
    public Task<Response> DeleteReviewsWithProductIdAsync(Guid id);
    public Task<Response> UpdateReviewAsync(UpdateReviewRequest request);
}