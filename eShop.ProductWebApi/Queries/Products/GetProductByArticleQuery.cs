using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByArticleQuery(long Article) : IRequest<Result<ProductDTO>>;

    public class GetProductByArticleQueryHandler(
        ProductDbContext context,
        ILogger<GetProductByArticleQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetProductByArticleQuery, Result<ProductDTO>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetProductByArticleQueryHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(GetProductByArticleQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find product with article {0}", request.Article);
            try
            {
                logger.LogInformation("Attempting to find product with article {article}", request.Article);

                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Article == request.Article);

                if (product is null)
                {
                    return logger.LogErrorWithException<ProductDTO>(new NotFoundProductException(request.Article), actionMessage);
                }

                var output = product.Category switch
                {
                    Category.Clothing => await MapCategory<ClothingEntity, ClothingDTO>(product),
                    Category.Shoes => await MapCategory<ShoesEntity, ShoesDTO>(product),
                    _ or Category.None => await MapCategory<ProductEntity, ProductDTO>(product),
                };

                logger.LogInformation("Successfully got product with article {Article}.", request.Article);
                return new(output);
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
                .FirstOrDefaultAsync(x => x.Article == productEntity.Article);

            return output!;
        }
    }
}
