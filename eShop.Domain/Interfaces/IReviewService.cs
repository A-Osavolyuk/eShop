using eShop.Domain.DTOs.Requests.Review;
using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface IReviewService
    {
        public Task<ResponseDTO> GetReviewListByProductIdAsync(Guid Id);
        public Task<ResponseDTO> CreateReviewAsync(CreateReviewRequest request);
        public Task<ResponseDTO> DeleteReviewsWithProductIdAsync(Guid Id);
        public Task<ResponseDTO> UpdateReviewAsync(UpdateReviewRequest request);
    }
}
