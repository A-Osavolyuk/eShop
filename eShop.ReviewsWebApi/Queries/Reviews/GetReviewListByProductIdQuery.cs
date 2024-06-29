using eShop.ReviewsWebApi.Repositories;

namespace eShop.ReviewsWebApi.Queries.Reviews
{
    public record GetReviewListByProductIdQuery(Guid Id) : IRequest<Result<IEnumerable<ReviewDTO>>>;

    public class GetReviewListByProductIdQueryHandler(IReviewRepository repository) : IRequestHandler<GetReviewListByProductIdQuery, Result<IEnumerable<ReviewDTO>>>
    {
        private readonly IReviewRepository repository = repository;

        public async Task<Result<IEnumerable<ReviewDTO>>> Handle(GetReviewListByProductIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetReviewListByProductIdAsync(request.Id);
            return result;
        }
    }
}
