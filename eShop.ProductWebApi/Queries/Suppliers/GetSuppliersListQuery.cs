using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Suppliers
{
    public record GetSuppliersListQuery : IRequest<Result<IEnumerable<SupplierDTO>>>;

    public class GetSuppliersListQueryHandler(ISuppliersRepository repository) : IRequestHandler<GetSuppliersListQuery, Result<IEnumerable<SupplierDTO>>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<IEnumerable<SupplierDTO>>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSuppliersListAsync();
            return result;
        }
    }
}
