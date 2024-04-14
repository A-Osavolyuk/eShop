using eShop.ProductWebApi.Repositories;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Products
{
    public record DeleteProductByIdCommand(Guid Id) : IRequest<Result<Unit>>;
    public class DeleteProductByIdCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductByIdCommand, Result<Unit>>
    {
        private readonly IProductRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteProductByIdAsync(request.Id);
            return result;
        }
    }
}
