using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Suppliers
{
    public record GetSupplierByIdQuery(Guid Id) : IRequest<Result<SupplierDTO>>;

    public class GetSupplierByIdQueryHandler(ISupplierRepository repository) : IRequestHandler<GetSupplierByIdQuery, Result<SupplierDTO>>
    {
        public async Task<Result<SupplierDTO>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByIdAsync(request.Id);
            return result;
        }
    }
}
