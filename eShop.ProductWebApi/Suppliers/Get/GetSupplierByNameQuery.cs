using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSupplierByNameQuery(string Name) : IRequest<Result<SupplierDto>>;

    public class GetSupplierByNameQueryHandler(ISuppliersRepository repository, IMapper mapper) : IRequestHandler<GetSupplierByNameQuery, Result<SupplierDto>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SupplierDto>> Handle(GetSupplierByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByNameAsync(request.Name);

            return result.Match<Result<SupplierDto>>(s => new(mapper.Map<SupplierDto>(s)), f => new(f));
        }
    }
}