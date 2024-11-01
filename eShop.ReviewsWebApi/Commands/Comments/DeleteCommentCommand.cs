using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;

namespace eShop.ReviewsWebApi.Commands.Comments;

public record DeleteCommentCommand(DeleteCommentRequest Request) : IRequest<Result<DeleteCommentResponse>>;

public class DeleteCommentCommandHandler(
    ReviewDbContext context) : IRequestHandler<DeleteCommentCommand, Result<DeleteCommentResponse>>
{
    private readonly ReviewDbContext context = context;

    public async Task<Result<DeleteCommentResponse>> Handle(DeleteCommentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var comment = await context.Comments
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CommentId == request.Request.CommentId,
                    cancellationToken: cancellationToken);

            if (comment is null)
            {
                return new(new NotFoundException($"Cannot find comment with id: {request.Request.CommentId}."));
            }
            
            context.Comments.Remove(comment);
            await context.SaveChangesAsync(cancellationToken);

            return new(new DeleteCommentResponse()
            {
                Message = "Comment successfully deleted.",
                IsSucceeded = true
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}