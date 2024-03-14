namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto>>;

    public class GetCategoryByIdQueryHandler(ICategoriesRepository repository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly ICategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCategoryByIdAsync(request.Id);

            return result.Match<Result<CategoryDto>>(s => new(mapper.Map<CategoryDto>(s)), f => new(f));
        }
    }
}
