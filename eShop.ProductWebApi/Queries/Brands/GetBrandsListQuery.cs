using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandsListQuery : IRequest<Result<IEnumerable<BrandDTO>>>;

    public class GetBrandsListQueryHandler(
        IMapper mapper,
        ILogger<GetBrandsListQueryHandler> logger,
        ProductDbContext context) : IRequestHandler<GetBrandsListQuery, Result<IEnumerable<BrandDTO>>>
    {
        private readonly IMapper mapper = mapper;
        private readonly ILogger<GetBrandsListQueryHandler> logger = logger;
        private readonly ProductDbContext context = context;

        public async Task<Result<IEnumerable<BrandDTO>>> Handle(GetBrandsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Attempting to get list of all brands");

                var brands = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).ToListAsync();

                logger.LogInformation($"Successfully got list of all brands");

                return new(brands);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<BrandDTO>>(ex, new ActionMessage("get all brands"));
            }
        }
    }
}
