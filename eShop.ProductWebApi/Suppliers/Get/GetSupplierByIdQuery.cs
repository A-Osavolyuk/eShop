
using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSupplierByIdQuery(Guid Id) : IRequest<Result<SupplierDto>>;

    public class GetSupplierByIdQueryHandler(ISuppliersRepository repository, IMapper mapper) : IRequestHandler<GetSupplierByIdQuery, Result<SupplierDto>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SupplierDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByIdAsync(request.Id);

            return result.Match<Result<SupplierDto>>(s => new(mapper.Map<SupplierDto>(s)), f => new(f));
        }
    }
}
