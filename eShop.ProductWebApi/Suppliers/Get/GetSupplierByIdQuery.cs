
namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSupplierByIdQuery(Guid Id) : IRequest<Result<SupplierEntity>>;

    public class GetSupplierByIdQueryHandler(ISuppliersRepository repository) : IRequestHandler<GetSupplierByIdQuery, Result<SupplierEntity>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<SupplierEntity>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByIdAsync(request.Id);

            return result.Match<Result<SupplierEntity>>(s => new(s), f => new(f));
        }
    }
}
