using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandsListQuery : IRequest<Result<IEnumerable<BrandDTO>>>;

    public class GetBrandsListQueryHandler(IBrandsRepository repository) : IRequestHandler<GetBrandsListQuery, Result<IEnumerable<BrandDTO>>>
    {
        private readonly IBrandsRepository repository = repository;

        public async Task<Result<IEnumerable<BrandDTO>>> Handle(GetBrandsListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetBrandsListAsync();
            return result;
        }
    }
}
