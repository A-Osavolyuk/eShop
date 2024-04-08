
using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoryByNameQuery(string Name) : IRequest<Result<SubcategoryDto>>;

    public class GetSubcategoryByNameQueryHandler(ISubcategoriesRepository repository, IMapper mapper) : IRequestHandler<GetSubcategoryByNameQuery, Result<SubcategoryDto>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SubcategoryDto>> Handle(GetSubcategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSubcategoryByNameAsync(request.Name);

            return result.Match<Result<SubcategoryDto>>(s => new(mapper.Map<SubcategoryDto>(s)), f => new(f));
        }
    }
}
