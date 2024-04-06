
using eShop.Domain.DTOs;

namespace eShop.ProductWebApi.Subcategories.Get
{
    public record GetSubcategoryByIdQuery(Guid Id) : IRequest<Result<SubcategoryDto>>;

    public class GetSubcategoryByIdQueryHandler(ISubcategoriesRepository repository, IMapper mapper) : IRequestHandler<GetSubcategoryByIdQuery, Result<SubcategoryDto>>
    {
        private readonly ISubcategoriesRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SubcategoryDto>> Handle(GetSubcategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetSubcategoryByIdAsync(request.Id);

            return result.Match<Result<SubcategoryDto>>(s => new(mapper.Map<SubcategoryDto>(s)), f => new(f));
        }
    }
}
