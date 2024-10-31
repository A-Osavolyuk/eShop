using AutoMapper;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using eShop.Domain.Responses.Product;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record SearchProductByNameQuery(string Name) : IRequest<Result<SearchProductResponse>>;

    public class SearchProductByNameQueryHandler(
        ProductDbContext context,
        ILogger<SearchProductByNameQueryHandler> logger,
        IMapper mapper) : IRequestHandler<SearchProductByNameQuery, Result<SearchProductResponse>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<SearchProductByNameQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SearchProductResponse>> Handle(SearchProductByNameQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("search product(s) with name containing '{0}'", request.Name);
            try
            {
                logger.LogInformation("Attempting to search product(s) with name containing {Name}.", request.Name);

                var quantity = await context.Products.AsNoTracking().Where(x => x.Name.Contains(request.Name)).CountAsync();

                if (quantity > 0)
                {
                    logger.LogInformation("Successfully found product(s) with name containing {Name} in quantity: {quantity}.", request.Name, quantity);
                    return new(new SearchProductResponse() { Found = true, Count = quantity });
                }

                return logger.LogInformationWithException<SearchProductResponse>(
                    new NotFoundException($"Cannot find product containing name {request.Name}."), actionMessage);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<SearchProductResponse>(ex, actionMessage);
            }
        }
    }
}
