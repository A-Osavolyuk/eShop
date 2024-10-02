using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsListQuery : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsListQueryHandler(
        IMapper mapper,
        ILogger<GetProductsListQueryHandler> logger,
        ProductDbContext context) : IRequestHandler<GetProductsListQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IMapper mapper = mapper;
        private readonly ILogger<GetProductsListQueryHandler> logger = logger;
        private readonly ProductDbContext context = context;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Attempting to get list of all products.");
                var list = await context.Products
                    .AsNoTracking()
                    .ProjectTo<ProductDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();

                logger.LogInformation($"Successfully got list of all products.");
                return new(list);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<ProductDTO>>(ex, new ActionMessage("get list of all products"));
            }
        }
    }
}
