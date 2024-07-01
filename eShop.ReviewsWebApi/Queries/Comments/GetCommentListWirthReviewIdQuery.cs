using eShop.ReviewsWebApi.Repositories;

namespace eShop.ReviewsWebApi.Queries.Comments
{
    public record GetCommentListWirthReviewIdQuery(Guid Id) : IRequest<Result<IEnumerable<CommentDTO>>>;

    public class GetCommentListWirthReviewIdQueryHandler(ICommentRepository repository) : IRequestHandler<GetCommentListWirthReviewIdQuery, Result<IEnumerable<CommentDTO>>>
    {
        private readonly ICommentRepository repository = repository;

        public async Task<Result<IEnumerable<CommentDTO>>> Handle(GetCommentListWirthReviewIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCommentListWithReviewId(request.Id);
            return result;
        }
    }
}
