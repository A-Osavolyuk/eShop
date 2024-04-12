using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<Result<BrandDTO>>;

    public class GetBrandByIdQueryHandler(IBrandsRepository repository) : IRequestHandler<GetBrandByIdQuery, Result<BrandDTO>>
    {
        public async Task<Result<BrandDTO>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetBrandByIdAsync(request.Id);
            return result;
        }
    }
}
