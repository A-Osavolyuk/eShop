namespace eShop.Domain.Interfaces
{
    public interface ICommentService
    {
        public ValueTask<ResponseDto> GetCommentsAsync(Guid productId);
        public ValueTask<ResponseDto> CreateCommentAsync(CreateCommentRequest request);
        public ValueTask<ResponseDto> UpdateCommentAsync(UpdateCommentRequest request);
        public ValueTask<ResponseDto> DeleteCommentAsync(DeleteCommentsRequest request);
    }
}
