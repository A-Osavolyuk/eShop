using eShop.Domain.DTOs;
using eShop.Domain.Requests.Comments;

namespace eShop.Domain.Interfaces
{
    public interface ICommentService
    {
        public ValueTask<ResponseDTO> GetCommentsAsync(Guid productId);
        public ValueTask<ResponseDTO> CreateCommentAsync(CreateCommentRequest request);
        public ValueTask<ResponseDTO> UpdateCommentAsync(UpdateCommentRequest request);
        public ValueTask<ResponseDTO> DeleteCommentAsync(DeleteCommentsRequest request);
    }
}
