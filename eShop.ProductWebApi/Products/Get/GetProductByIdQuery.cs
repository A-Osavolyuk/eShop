
namespace eShop.ProductWebApi.Products.Get
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

    public class GetProductByIdQueryHandler(IProductsRepository repository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IProductsRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetProductByIdAsync(request.Id);
            return result.Match<Result<ProductDto>>(s => new(mapper.Map<ProductDto>(s)), f => new(f));
        }
    }
}
