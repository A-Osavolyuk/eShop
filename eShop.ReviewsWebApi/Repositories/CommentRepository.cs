using AutoMapper.QueryableExtensions;

namespace eShop.ReviewsWebApi.Repositories
{
    public class CommentRepository(ReviewDbContext context, IMapper mapper, ILogger<CommentRepository> logger) : ICommentRepository
    {
        private readonly ReviewDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<CommentRepository> logger = logger;

        public async ValueTask<Result<IEnumerable<CommentDTO>>> GetCommentListWithReviewId(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get comments with review id: {Id}");

                var list = await context.Comments.Where(x => x.ReviewId == Id).ProjectTo<CommentDTO>(mapper.ConfigurationProvider).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting comments with review id: {Id} with error message: {ex}");
                return new(ex);
            }
        }
    }

    public interface ICommentRepository
    {
        public ValueTask<Result<IEnumerable<CommentDTO>>> GetCommentListWithReviewId(Guid Id);
    }
}
