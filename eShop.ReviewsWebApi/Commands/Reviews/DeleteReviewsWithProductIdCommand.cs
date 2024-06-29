using eShop.ReviewsWebApi.Repositories;

namespace eShop.ReviewsWebApi.Commands.Reviews
{
    public record DeleteReviewsWithProductIdCommand(Guid ProductId) : IRequest<Result<Unit>>;

    public class DeleteReviewsWithProductIdCommandHandler(IReviewRepository repository) : IRequestHandler<DeleteReviewsWithProductIdCommand, Result<Unit>>
    {
        private readonly IReviewRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteReviewsWithProductIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteReviewsWithProductIdAsync(request.ProductId);
            return result;
        }
    }
}
