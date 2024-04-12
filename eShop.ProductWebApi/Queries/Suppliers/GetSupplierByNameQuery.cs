using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Suppliers
{
    public record GetBrandByNameQuery(string Name) : IRequest<Result<SupplierDTO>>;

    public class GetSupplierByNameQueryHandler(ISupplierRepository repository) : IRequestHandler<GetBrandByNameQuery, Result<SupplierDTO>>
    {
        public async Task<Result<SupplierDTO>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByNameAsync(request.Name);
            return result;
        }
    }
}
