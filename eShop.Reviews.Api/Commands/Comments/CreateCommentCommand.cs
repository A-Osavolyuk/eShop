using eShop.Domain.Requests.Api.Comments;
using eShop.Domain.Responses.Api.Comments;
using eShop.Reviews.Api.Data;

namespace eShop.Reviews.Api.Commands.Comments;

internal sealed record CreateCommentCommand(CreateCommentRequest Request) : IRequest<Result<CreateCommentResponse>>;

internal sealed class CreateCommentCommandHandler(
    AppDbContext context,
    ILogger<CreateCommentCommandHandler> logger) : IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse>>
{
    private readonly AppDbContext context = context;
    private readonly ILogger<CreateCommentCommandHandler> logger = logger;

    public async Task<Result<CreateCommentResponse>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = CommentMapper.ToCommentEntity(request.Request);
        await context.Comments.AddAsync(comment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new(new CreateCommentResponse()
        {
            Message = "Comment was successfully created.",
        });
    }
}