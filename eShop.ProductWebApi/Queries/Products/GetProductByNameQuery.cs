using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByNameQuery(string Name) : IRequest<Result<ProductDTO>>;

    public class GetProductByNameQueryHandler(
        ProductDbContext context,
        ILogger<GetProductByNameQueryHandler> logger,
        IMapper mapper
        ) : IRequestHandler<GetProductByNameQuery, Result<ProductDTO>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetProductByNameQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find product with name {0}", request.Name);
            try
            {
                logger.LogInformation("Attempting to find product with name {name}", request.Name);

                var product = await context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

                if (product is null)
                {
                    return logger.LogInformationWithException<ProductDTO>(
                        new NotFoundException($"Cannot find product {request.Name}"), actionMessage);
                }

                var output = product.Category switch
                {
                    Category.Clothing => await MapCategory<ClothingEntity, ClothingDTO>(product),
                    Category.Shoes => await MapCategory<ShoesEntity, ShoesDTO>(product),
                    _ or Category.None => await MapCategory<ProductEntity, ProductDTO>(product),
                };

                logger.LogInformation("Successfylly found product with name {name}", request.Name);
                return new(output!);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ProductDTO>(ex, actionMessage);
            }
        }

        private async ValueTask<ProductDTO> MapCategory<TEntity, TOutput>(ProductEntity productEntity) where TEntity : ProductEntity where TOutput : ProductDTO
        {
            var output = await context.Products
                .AsNoTracking()
                .OfType<TEntity>()
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Name == productEntity.Name);

            return output!;
        }
    }
}
