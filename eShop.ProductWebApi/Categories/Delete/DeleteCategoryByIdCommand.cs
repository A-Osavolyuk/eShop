namespace eShop.ProductWebApi.Categories.Delete
{
    public record DeleteCategoryByIdCommand(Guid Id) : IRequest<Result<LanguageExt.Unit>>;

    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Result<LanguageExt.Unit>>
    {
        private readonly ICategoriesRepository repository;

        public DeleteCategoryByIdCommandHandler(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Unit>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteCategoryByIdAsync(request.Id);

            return result.Match<Result<Unit>>(s => new(s), f => new(f));
        }
    }
}
