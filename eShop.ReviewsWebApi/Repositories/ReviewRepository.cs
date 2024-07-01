using eShop.Domain.Entities;

namespace eShop.ReviewsWebApi.Repositories
{
    public class ReviewRepository(
        ReviewDbContext context,
        IMapper mapper,
        ILogger<ReviewRepository> logger) : IReviewRepository
    {
        private readonly ReviewDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<ReviewRepository> logger = logger;

        public async Task<Result<Unit>> CreateReviewAsync(CreateReviewRequest request)
        {
            try
            {
                logger.LogInformation("Trying to create review");

                var entity = mapper.Map<Review>(request);
                await context.Reviews.AddAsync(entity);
                await context.SaveChangesAsync();

                logger.LogInformation("Review was successfully created");
                return new(new Unit());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating review with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async Task<Result<Unit>> DeleteReviewsWithProductIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete reviews with product id: {Id}");

                var reviews = await context.Reviews.AsNoTracking().Where(x => x.ReviewId == Id).ToListAsync();

                if (reviews.Any()) 
                {
                    context.Reviews.RemoveRange(reviews);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Reviews with product id: {Id} were successfully deleted");
                    return new(new Unit());
                }

                var notDeletedException = new NotDeletedReviewsException(Id);
                logger.LogError($"Failed on deleting reviews with product id: {Id} with error message: {notDeletedException}")
                return new(notDeletedException);

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting reviews with product id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async Task<Result<IEnumerable<ReviewDTO>>> GetReviewListByProductIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get reviews with product id: {Id}");

                if (context.Reviews.Any(x => x.ProductId == Id))
                {
                    var list = await context.Reviews
                        .AsNoTracking()
                        .Where(x => x.ProductId == Id)
                        .Select(x => new ReviewDTO()
                        {
                            ReviewId = x.ProductId,
                            ProductId = x.ProductId,
                            UserId = x.UserId,
                            CreatedAt = x.CreatedAt,
                            Rating = x.Rating,
                            Text = x.Text,
                            UserName = x.UserName,
                            Comments = x.Comments.Select(x => new CommentDTO()
                            {
                                CommentId = x.CommentId,
                                CreatedAt = x.CreatedAt,
                                ReviewId = x.ReviewId,
                                Text = x.Text,
                                UserId = x.UserId,
                                UserName = x.UserName
                            })
                        })
                        .ToListAsync();

                    logger.LogInformation($"Successfully got reviews with product id: {Id}");

                    return new(list);
                }

                var notFoundException = new NotFoundReviewException(Id);
                logger.LogError($"Failed on getting reviews with product id: {Id} with error message: {notFoundException.Message}");
                return new(notFoundException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting reviews with product id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async Task<Result<Unit>> UpdateReviewAsync(UpdateReviewRequest request)
        {
            try
            {
                var review = await context.Reviews.FirstOrDefaultAsync(x => x.ReviewId == request.ReviewId);

                if (review is not null)
                {
                    if(review.UserId == request.UserId)
                    {
                        var newReview = mapper.Map<Review>(request) with { UpdatedAt = DateTime.UtcNow };

                        context.Reviews.Update(newReview);
                        await context.SaveChangesAsync();

                        return new(new Unit());
                    }

                    return new(new DeniedUpdateException());
                }

                var notFoundException = new NotFoundReviewException(request.ReviewId);
                logger.LogError($"Failed no updating review with id: {request.ReviewId} with error message: {notFoundException.Message}");
                return new(notFoundException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating review with id: {request.ReviewId} with error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IReviewRepository
    {
        public Task<Result<IEnumerable<ReviewDTO>>> GetReviewListByProductIdAsync(Guid Id);
        public Task<Result<Unit>> CreateReviewAsync(CreateReviewRequest request);
        public Task<Result<Unit>> DeleteReviewsWithProductIdAsync(Guid Id);
        public Task<Result<Unit>> UpdateReviewAsync(UpdateReviewRequest request);
    }
}
