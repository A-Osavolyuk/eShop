using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using MediatR;

namespace eShop.ProductWebApi.Queries.Products
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDTO>>;

    public class GetProductByIdQueryHandler(
        IMapper mapper,
        ProductDbContext context,
        ILogger<GetProductByIdQueryHandler> logger) : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
    {
        private readonly IMapper mapper = mapper;
        private readonly ProductDbContext context = context;
        private readonly ILogger<GetProductByIdQueryHandler> logger = logger;

        public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("find product with ID {0}", request.Id);
            try
            {
                logger.LogInformation("Attempting to find product with ID {id}", request.Id);

                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product is null)
                {
                    return logger.LogErrorWithException<ProductDTO>(new NotFoundProductException(request.Id), actionMessage);
                }

                var output = product.Category switch
                {
                    Category.Clothing => await MapCategory<Clothing, ClothingDTO>(product),
                    Category.Shoes => await MapCategory<Shoes, ShoesDTO>(product),
                    _ or Category.None => await MapCategory<Product, ProductDTO>(product),
                };

                logger.LogInformation("Successfylly found product with ID {id}", request.Id);
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
                .FirstOrDefaultAsync(x => x.Id == product.Id);

            return output!;
        }
    }
}
