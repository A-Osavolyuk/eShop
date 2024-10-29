using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductsByVariantIdQuery(Guid VariantId) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class GetProductsByVariantIdQueryHandler(
        IMapper mapper,
        ProductDbContext context,
        ILogger<GetProductsByVariantIdQueryHandler> logger) : IRequestHandler<GetProductsByVariantIdQuery, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IMapper mapper = mapper;
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetProductsByVariantIdQueryHandler> logger = logger;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(GetProductsByVariantIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find product with variant ID {0}", request.VariantId);
            try
            {
                logger.LogInformation("Attempting to find products with variant ID {id}.", request.VariantId);

                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.VariantId == request.VariantId);

                if (product is null)
                {
                    return logger.LogErrorWithException<IEnumerable<ProductDTO>>(new NotFoundProductGroupException(request.VariantId), actionMessage);
                }

                var output = product.Category switch
                {
                    Category.Clothing => await MapProducts<ClothingEntity, ClothingDTO>(request.VariantId),
                    Category.Shoes => await MapProducts<ShoesEntity, ShoesDTO>(request.VariantId),
                    Category.None or _ => await MapProducts<ProductEntity, ProductDTO>(request.VariantId),
                };

                logger.LogInformation("Successfully found products with variant ID {id}.", request.VariantId);
                return new(output);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<IEnumerable<ProductDTO>>(ex, actionMessage);
            }
        }

        private async ValueTask<IEnumerable<ProductDTO>> MapProducts<TEntity, TOutput>(Guid VariantId) where TEntity : ProductEntity where TOutput : ProductDTO
        {
            var output = context.Products
                .AsNoTracking()
                .Where(x => x.VariantId == VariantId)
                .OfType<TEntity>()
                .ProjectTo<TOutput>(mapper.ConfigurationProvider);

            return await output.ToListAsync();
        }
    }
}
