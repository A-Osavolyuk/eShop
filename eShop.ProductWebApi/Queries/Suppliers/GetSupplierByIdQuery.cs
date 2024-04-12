using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Suppliers
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<Result<SupplierDTO>>;

    public class GetSupplierByIdQueryHandler(ISupplierRepository repository) : IRequestHandler<GetBrandByIdQuery, Result<SupplierDTO>>
    {
        public async Task<Result<SupplierDTO>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByIdAsync(request.Id);
            return result;
        }
    }
}
