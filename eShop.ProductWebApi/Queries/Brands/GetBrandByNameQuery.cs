using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.ProductWebApi.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandByNameQuery(string Name) : IRequest<Result<BrandDTO>>;

    public class GetBrandByNameQueryHandler(
        ProductDbContext context,
        ILogger<GetBrandByNameQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetBrandByNameQuery, Result<BrandDTO>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetBrandByNameQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<BrandDTO>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find brand with name {name}", request.Name);
            try
            {
                logger.LogInformation("Attempting to find brand with name {name}", request.Name);

                var brand = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == request.Name);

                if (brand is null)
                {
                    return logger.LogErrorWithException<BrandDTO>(new NotFoundBrandException(request.Name), actionMessage);
                }

                logger.LogInformation("Successfully found brand with name {name}", request.Name);
                return new(brand);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<BrandDTO>(ex, actionMessage);
            }
        }
    }
}
