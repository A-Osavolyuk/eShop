
namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductByNameQuery(string Name) : IRequest<Result<ProductDto>>;

    public class GetProductByNameQueryHandler(IProductsRepository repository, IMapper mapper) : IRequestHandler<GetProductByNameQuery, Result<ProductDto>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByNameAsync(request.Name);
            return result.Match<Result<ProductDto>>(s => new(mapper.Map<ProductDto>(s)), f => new(f));
        }
    }
}
