﻿using AutoMapper.QueryableExtensions;
using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;

namespace eShop.ReviewsWebApi.Queries.Comments;

public record GetCommentsQuery(GetCommentsRequest Request) : IRequest<Result<GetCommentsResponse>>;

public class GetCommentsQueryHandler(
    ReviewDbContext context,
    IMapper mapper) : IRequestHandler<GetCommentsQuery, Result<GetCommentsResponse>>
{
    private readonly ReviewDbContext context = context;
    private readonly IMapper mapper = mapper;

    public async Task<Result<GetCommentsResponse>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var commentsList = await context.Comments
                .AsNoTracking()
                .Where(x => x.ProductId == request.Request.ProductId)
                .ToListAsync(cancellationToken);

            var response = await commentsList
                .AsQueryable()
                .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new(new GetCommentsResponse()
            {
                Comments = response,
                Message = "Successfully found comments",
                IsSucceeded = true
            });
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}