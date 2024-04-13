using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;
using eShop.ProductWebApi.Exceptions.Suppliers;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Repositories
{
    public class SuppliersRepository(ProductDbContext context, IMapper mapper) : ISuppliersRepository
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

        public async ValueTask<Result<SupplierDTO>> CreateSupplierAsync(Supplier Supplier)
        {
            try
            {
                var data = (await context.Suppliers.AddAsync(Supplier)).Entity;
                var result = await context.SaveChangesAsync();
                var output = mapper.Map<SupplierDTO>(data);

                return result > 0 ? new(output) : new(new NotCreatedSupplierException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteSupplierAsync(Guid Id)
        {
            try
            {
                var Supplier = await context.Suppliers.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == Id);

                if (Supplier is not null)
                {
                    context.Suppliers.Remove(Supplier);
                    var result = await context.SaveChangesAsync();

                    return result > 0 ? new(new Unit()) : new(new NotDeletedSupplierException());
                }

                return new(new NotFoundSupplierException(Id));

            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierDTO>> UpdateSupplierAsync(Supplier Supplier)
        {
            try
            {
                var exists = await context.Suppliers.AsNoTracking().AnyAsync(_ => _.Id == Supplier.Id);

                if (exists)
                {
                    var data = context.Suppliers.Update(Supplier).Entity;
                    var result = await context.SaveChangesAsync();
                    var output = mapper.Map<SupplierDTO>(data);

                    return result > 0 ? new(output) : new(new NotUpdatedSupplierException());
                }

                return new(new NotFoundSupplierException(Supplier.Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }

    public interface ISuppliersRepository
    {
        public ValueTask<Result<IEnumerable<SupplierDTO>>> GetSuppliersListAsync();
        public ValueTask<Result<SupplierDTO>> GetSupplierByIdAsync(Guid Id);
        public ValueTask<Result<SupplierDTO>> GetSupplierByNameAsync(string Name);
        public ValueTask<Result<SupplierDTO>> CreateSupplierAsync(Supplier Supplier);
        public ValueTask<Result<Unit>> DeleteSupplierAsync(Guid Id);
        public ValueTask<Result<SupplierDTO>> UpdateSupplierAsync(Supplier Supplier);
    }
}
