using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<Result<BrandDTO>>;

    public class GetBrandByIdQueryHandler(
        ILogger<GetBrandByIdQueryHandler> logger,
        ProductDbContext context,
        IMapper mapper) : IRequestHandler<GetBrandByIdQuery, Result<BrandDTO>>
    {
        private readonly ILogger<GetBrandByIdQueryHandler> logger = logger;
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async Task<Result<BrandDTO>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find brand with ID {0}", request.Id);
            try
            {
                logger.LogInformation("Attempting to find brand with ID {brandId}", request.Id);

                var brand = await context.Brands.AsNoTracking()
                    .ProjectTo<BrandDTO>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (brand is null)
                {
                    return logger.LogInformationWithException<BrandDTO>(
                        new NotFoundException($"Cannot find brand with ID {request.Id}."), actionMessage);
                }

                logger.LogInformation("Successfully found brand with ID {id}", request.Id);
                return new(brand);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<BrandDTO>(ex, actionMessage);
            }
        }
    }
}