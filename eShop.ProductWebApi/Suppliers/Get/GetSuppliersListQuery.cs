
using AutoMapper.QueryableExtensions;

namespace eShop.ProductWebApi.Suppliers.Get
{
    public record GetSuppliersListQuery : IRequest<Result<IEnumerable<SupplierDto>>>;

    public class GetSuppliersListQueryHandler(ISuppliersRepository repository, IMapper mapper) : IRequestHandler<GetSuppliersListQuery, Result<IEnumerable<SupplierDto>>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<SupplierDto>>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllSuppliersAsync();

            return result.Match<Result<IEnumerable<SupplierDto>>>(
                s => new(s.AsQueryable().ProjectTo<SupplierDto>(mapper.ConfigurationProvider).ToList()), 
                f => new(f));
        }
    }
}
