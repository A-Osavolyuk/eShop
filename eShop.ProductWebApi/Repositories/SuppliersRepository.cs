using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;

namespace eShop.ProductWebApi.Repositories
{
    public class SuppliersRepository(ProductDbContext context, IMapper mapper) : ISupplierRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<SupplierDTO>> GetSupplierByIdAsync(Guid Id)
        {
            try
            {
                var supplier = await context.Suppliers.AsNoTracking().ProjectTo<SupplierDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);
                return supplier is null ? new(new NotFoundSupplierException(Id)) : new(supplier);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierDTO>> GetSupplierByNameAsync(string Name)
        {
            try
            {
                var supplier = await context.Suppliers.AsNoTracking().ProjectTo<SupplierDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name);
                return supplier is null ? new(new NotFoundSupplierException(Name)) : new(supplier);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

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
        public ValueTask<Result<SupplierDTO>> GetSupplierByIdAsync(Guid Id);
        public ValueTask<Result<SupplierDTO>> GetSupplierByNameAsync(string Name);
    }
}
