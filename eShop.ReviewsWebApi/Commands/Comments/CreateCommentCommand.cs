﻿using eShop.Domain.Entities;
using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;
using FluentValidation;

namespace eShop.ReviewsWebApi.Commands.Comments;

public record CreateCommentCommand(CreateCommentRequest Request) : IRequest<Result<CreateCommentResponse>>;

public class CreateCommentCommandHandler(
    ReviewDbContext context,
    ILogger<CreateCommentCommandHandler> logger,
    IMapper mapper) : IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse>>
{
    private readonly ReviewDbContext context = context;
    private readonly ILogger<CreateCommentCommandHandler> logger = logger;
    private readonly IMapper mapper = mapper;

    public async Task<Result<CreateCommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        { 
            var comment = mapper.Map<CommentEntity>(request.Request);
            await context.Comments.AddAsync(comment, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            
            return new(new CreateCommentResponse()
            {
                Message = "Comment was successfully created.",
                IsSucceeded = true
            });
        }
        catch (Exception ex)
        {
            return new(ex) ;
        }
    }
}