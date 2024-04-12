using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace eShop.ProductWebApi.Repositories
{
    public class SuppliersRepository(ProductDbContext context, IMapper mapper) : ISupplierRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<IEnumerable<SupplierDTO>>> GetSuppliersListAsync()
        {
            try
            {
                return new(await context.Suppliers
                    .AsNoTracking()
                    .ProjectTo<SupplierDTO>(mapper.ConfigurationProvider)
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }

    public interface ISupplierRepository
    {
        public ValueTask<Result<IEnumerable<SupplierDTO>>> GetSuppliersListAsync();
    }
}
