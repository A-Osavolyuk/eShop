
namespace eShop.ProductWebApi.Subcategories.Delete
{
    public record DeleteSubcategoryByIdCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteSubcategoryByIdCommandHandler(ISubcategoriesRepository repository) : IRequestHandler<DeleteSubcategoryByIdCommand, Result<Unit>>
    {
        private readonly ISubcategoriesRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteSubcategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteSubcategoryByIdAsync(request.Id);

            return result.Match<Result<Unit>>(s => new(s), f => new(f));
        }
    }
}
