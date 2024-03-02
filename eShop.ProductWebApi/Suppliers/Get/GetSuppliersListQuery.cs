
namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSuppliersListQuery : IRequest<Result<IEnumerable<SupplierEntity>>>;

    public class GetSuppliersListQueryHandler(ISuppliersRepository repository) : IRequestHandler<GetSuppliersListQuery, Result<IEnumerable<SupplierEntity>>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<IEnumerable<SupplierEntity>>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllSuppliersAsync();

            return result.Match<Result<IEnumerable<SupplierEntity>>>(s => new(s), f => new(f));
        }
    }
}
