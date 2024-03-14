using AutoMapper.QueryableExtensions;

namespace eShop.ProductWebApi.Categories.Get
{
    public record GetCategoriesListQuery : IRequest<Result<IEnumerable<CategoryDto>>>;

    public class GetCategoriesListQueryHandler(ICategoriesRepository categories, IMapper mapper) : IRequestHandler<GetCategoriesListQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly ICategoriesRepository repository = categories;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllCategoriesAsync();

            return result.Match<Result<IEnumerable<CategoryDto>>>(
                s => new(s.AsQueryable().ProjectTo<CategoryDto>(mapper.ConfigurationProvider).ToList()), 
                f => new(f));
        }
    }
}
