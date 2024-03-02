namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSupplierByNameQuery(string Name) : IRequest<Result<SupplierEntity>>;

    public class GetSupplierByNameQueryHandler(ISuppliersRepository repository) : IRequestHandler<GetSupplierByNameQuery, Result<SupplierEntity>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<SupplierEntity>> Handle(GetSupplierByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSupplierByNameAsync(request.Name);

            return result.Match<Result<SupplierEntity>>(s => new(s), f => new(f));
        }
    }
}