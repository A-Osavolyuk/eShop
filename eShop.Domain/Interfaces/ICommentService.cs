using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface ICommentService
    {
        public ValueTask<ResponseDTO> GetCommentListWithReviewId(Guid Id);
    }
}
