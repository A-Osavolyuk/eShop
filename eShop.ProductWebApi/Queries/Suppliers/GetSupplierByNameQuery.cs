using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Suppliers
{
    public record GetSupplierByNameQuery(string Name) : IRequest<Result<SupplierDTO>>;

    public class GetSupplierByNameQueryHandler(ISupplierRepository repository) : IRequestHandler<GetSupplierByNameQuery, Result<SupplierDTO>>
    {
        public async Task<Result<SupplierDTO>> Handle(GetSupplierByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByNameAsync(request.Name);
            return result;
        }
    }
}
