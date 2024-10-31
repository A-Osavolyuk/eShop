using AutoMapper;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using eShop.Domain.Responses.Product;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByArticleQuery(long Article) : IRequest<Result<SearchProductResponse>>;

    public class SearchProductByArticleQueryHandler(
        IMapper mapper,
        ILogger<SearchProductByArticleQueryHandler> logger,
        ProductDbContext context) : IRequestHandler<SearchProductByArticleQuery, Result<SearchProductResponse>>
    {
        private readonly IMapper mapper = mapper;
        private readonly ILogger<SearchProductByArticleQueryHandler> logger = logger;
        private readonly ProductDbContext context = context;

        public async Task<Result<SearchProductResponse>> Handle(SearchProductByArticleQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("search product with article {0}", request.Article);
            try
            {
                logger.LogInformation("Attempting to search product with article {Article}.", request.Article);

                var product = await context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Article == request.Article, cancellationToken: cancellationToken);

                if (product is null)
                {
                    return logger.LogInformationWithException<SearchProductResponse>(
                        new NotFoundException($"Cannot find product with article {request.Article}"), actionMessage);
                }

                logger.LogInformation("Successfully found product with article {Article}.", request.Article);
                return new(new SearchProductResponse() { Found = true, Count = 1 });
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<SearchProductResponse>(ex, actionMessage);
            }
        }
    }
}
