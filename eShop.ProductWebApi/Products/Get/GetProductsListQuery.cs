
using AutoMapper.QueryableExtensions;

namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductsListQuery : IRequest<Result<IEnumerable<ProductDto>>>;

    public class GetProductsListQueryHandler(IProductsRepository repository, IMapper mapper) : IRequestHandler<GetProductsListQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllProductsAsync();

            return result.Match<Result<IEnumerable<ProductDto>>>(
                s => new(s.AsQueryable().ProjectTo<ProductDto>(mapper.ConfigurationProvider).ToList()),
                f => new(f));
        }
    }
}
