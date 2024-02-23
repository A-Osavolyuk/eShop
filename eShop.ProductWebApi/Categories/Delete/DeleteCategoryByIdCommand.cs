namespace eShop.ProductWebApi.Categories.Delete
{
    public record DeleteCategoryByIdCommand(Guid Id) : IRequest<Result<bool>>;

    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Result<bool>>
    {
        private readonly ICategoriesRepository repository;

        public DeleteCategoryByIdCommandHandler(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<bool>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteCategoryByIdAsync(request.Id);

            return result.Match<Result<bool>>(s => new(s), f => new(f));
        }
    }
}
