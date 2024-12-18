using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces
{
    public interface ICommentService
    {
        public ValueTask<Response> GetCommentsAsync(Guid productId);
        public ValueTask<Response> CreateCommentAsync(CreateCommentRequest request);
        public ValueTask<Response> UpdateCommentAsync(UpdateCommentRequest request);
        public ValueTask<Response> DeleteCommentAsync(DeleteCommentsRequest request);
    }
}
