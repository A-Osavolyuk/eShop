using eShop.Application.Mapping;
using eShop.Domain.Entities;
using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;
using eShop.ReviewsApi.Data;

namespace eShop.ReviewsApi.Commands.Comments;

internal sealed record CreateCommentCommand(CreateCommentRequest Request) : IRequest<Result<CreateCommentResponse>>;

internal sealed class CreateCommentCommandHandler(
    ReviewDbContext context,
    ILogger<CreateCommentCommandHandler> logger) : IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse>>
{
    private readonly ReviewDbContext context = context;
    private readonly ILogger<CreateCommentCommandHandler> logger = logger;

    public async Task<Result<CreateCommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        { 
            var comment = CommentMapper.ToCommentEntity(request.Request);
            await context.Comments.AddAsync(comment, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            
            return new(new CreateCommentResponse()
            {
                Message = "Comment was successfully created.",
            });
        }
        catch (Exception ex)
        {
            return new(ex) ;
        }
    }
}