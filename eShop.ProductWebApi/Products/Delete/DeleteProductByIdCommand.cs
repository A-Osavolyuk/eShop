
namespace eShop.ProductWebApi.Products.Delete
{
    public record DeleteProductByIdCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteProductByIdCommandHandler(IProductsRepository repository) : IRequestHandler<DeleteProductByIdCommand, Result<Unit>>
    {
        private readonly IProductsRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteProductByIdAsync(request.Id);

            return result.Match<Result<Unit>>(s => new(s), f => new(f));
        }
    }
}
