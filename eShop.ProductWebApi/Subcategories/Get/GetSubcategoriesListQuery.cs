
using AutoMapper.QueryableExtensions;

namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoriesListQuery : IRequest<Result<IEnumerable<SubcategoryDto>>>;

    public class GetSubcategoriesListQueryHandler(
        ISubcategoriesRepository repository, 
        IMapper mapper) : IRequestHandler<GetSubcategoriesListQuery, Result<IEnumerable<SubcategoryDto>>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<SubcategoryDto>>> Handle(GetSubcategoriesListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllSubcategoriesAsync();

            return result.Match<Result<IEnumerable<SubcategoryDto>>>(
                s => new(s.AsQueryable().ProjectTo<SubcategoryDto>(mapper.ConfigurationProvider).ToList()),
                f => new(f));
        }
    }
}
