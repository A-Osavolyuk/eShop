using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsByNameQuery(string Name) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsByNameQueryHandler(
        ProductDbContext context,
        ILogger<GetProductsByNameQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetProductsByNameQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetProductsByNameQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Attempting to find products with name containing '{name}'.", request.Name);

                var list = await context.Products
                    .AsNoTracking()
                    .Where(x => x.Name.Contains(request.Name))
                    .ProjectTo<ProductDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();

                logger.LogInformation("Successfully got products with name containing '{name}'.", request.Name);
                return new(list);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<ProductDTO>>(ex, new ActionMessage("find product containing '{0}'", request.Name));
            }
        }
    }
}
