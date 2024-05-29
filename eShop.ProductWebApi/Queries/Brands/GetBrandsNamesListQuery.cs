using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Queries.Brands
{
    public record GetBrandsNamesListQuery : IRequest<Result<IEnumerable<string>>>;

    public class GetBrandsNamesListQueryHandler(IBrandsRepository repository) : IRequestHandler<GetBrandsNamesListQuery, Result<IEnumerable<string>>>
    {
        private readonly IBrandsRepository repository = repository;

        public async Task<Result<IEnumerable<string>>> Handle(GetBrandsNamesListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetBrandsNamesListAsync();
            return result;
        }
    }
}
