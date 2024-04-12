using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandByNameQuery(string Name) : IRequest<Result<BrandDTO>>;

    public class GetBrandByNameQueryHandler(IBrandsRepository repository) : IRequestHandler<GetBrandByNameQuery, Result<BrandDTO>>
    {
        public async Task<Result<BrandDTO>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetBrandByNameAsync(request.Name);
            return result;
        }
    }
}
