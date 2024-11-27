namespace eShop.Domain.Interfaces
{
    public interface IReviewService
    {
        public Task<ResponseDto> GetReviewListByProductIdAsync(Guid Id);
        public Task<ResponseDto> CreateReviewAsync(CreateReviewRequest request);
        public Task<ResponseDto> DeleteReviewsWithProductIdAsync(Guid Id);
        public Task<ResponseDto> UpdateReviewAsync(UpdateReviewRequest request);
    }
}
