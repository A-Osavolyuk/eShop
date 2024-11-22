using eShop.Application.Mapping;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;
using eShop.ReviewsApi.Data;

namespace eShop.ReviewsApi.Commands.Comments;

internal sealed record UpdateCommentCommand(UpdateCommentRequest Request) : IRequest<Result<UpdateCommentResponse>>;

internal sealed class UpdateCommentCommandHandler(
    ReviewDbContext context) : IRequestHandler<UpdateCommentCommand, Result<UpdateCommentResponse>>
{
    private readonly ReviewDbContext context = context;

    public async Task<Result<UpdateCommentResponse>> Handle(UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var comment = await context.Comments
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CommentId == request.Request.CommentId, cancellationToken);

            if (comment is null)
            {
                return new(new NotFoundException($"Cannot find comment with id: {request.Request.CommentId}."));
            }

            var newComment = CommentMapper.ToCommentEntity(request.Request) with { UpdatedAt = DateTime.UtcNow };
            context.Comments.Update(newComment);
            await context.SaveChangesAsync(cancellationToken);

            return new Result<UpdateCommentResponse>(new UpdateCommentResponse()
            {
                Message = "Comment was successfully updated.",
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}