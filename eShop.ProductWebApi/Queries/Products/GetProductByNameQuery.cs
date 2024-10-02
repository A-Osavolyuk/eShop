using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
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

                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == request.Name);

                if (product is null)
                {
                    return logger.LogErrorWithException<ProductDTO>(new NotFoundProductException(request.Name), actionMessage);
                }

                var output = product.Category switch
                {
                    Category.Clothing => await MapCategory<Clothing, ClothingDTO>(product),
                    Category.Shoes => await MapCategory<Shoes, ShoesDTO>(product),
                    _ or Category.None => await MapCategory<Product, ProductDTO>(product),
                };

                logger.LogInformation("Successfylly found product with name {name}", request.Name);
                return new(output!);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<ProductDTO>(ex, actionMessage);
            }
        }

        private async ValueTask<ProductDTO> MapCategory<TEntity, TOutput>(Product product) where TEntity : Product where TOutput : ProductDTO
        {
            var output = await context.Products
                .AsNoTracking()
                .OfType<TEntity>()
                .ProjectTo<TOutput>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Name == product.Name);

            return output!;
        }
    }
}
