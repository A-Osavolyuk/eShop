
namespace eShop.ProductWebApi.Suppliers.Delete
{
    public record DeleteSupplierByIdCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteSupplierByIdCommandHandler(ISuppliersRepository repository) : IRequestHandler<DeleteSupplierByIdCommand, Result<Unit>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteSupplierByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteSupplierByIdAsync(request.Id);

            return result.Match<Result<Unit>>(s => new(s), f => new(f));
        }
    }
}
