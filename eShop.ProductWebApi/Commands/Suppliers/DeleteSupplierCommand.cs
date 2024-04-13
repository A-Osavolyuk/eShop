using eShop.ProductWebApi.Repositories;
using Unit = LanguageExt.Unit;
using MediatR;

namespace eShop.ProductWebApi.Commands.Suppliers
{
    public record DeleteSupplierCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteBrandCommandHandler(ISuppliersRepository repository) : IRequestHandler<DeleteSupplierCommand, Result<Unit>>
    {
        private readonly ISuppliersRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteSupplierAsync(request.Id);
            return result;
        }
    }
}
