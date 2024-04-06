using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoryByNameQuery(string name) : IRequest<Result<CategoryDto>>;

    public class GetCategoryByNameQueryHandler(ICategoriesRepository repository, IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, Result<CategoryDto>>
    {
        private readonly ICategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<CategoryDto>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCategoryByNameAsync(request.name);

            return result.Match<Result<CategoryDto>>(s => new(mapper.Map<CategoryDto>(s)), f => new(f));
        }
    }
}
