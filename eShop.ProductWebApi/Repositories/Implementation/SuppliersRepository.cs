
namespace eShop.ProductWebApi.Repositories.Implementation
{
    public class SuppliersRepository(ProductDbContext dbContext) : ISuppliersRepository
    {
        private readonly ProductDbContext dbContext = dbContext;

        public async ValueTask<Result<SupplierEntity>> CreateSupplierAsync(SupplierEntity Supplier)
        {
            try
            {
                var entity = (await dbContext.Suppliers.AddAsync(Supplier)).Entity;
                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? new(entity) : new(new NotCreatedSupplierException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteSupplierByIdAsync(Guid Id)
        {
            try
            {
                var supplier = await dbContext.Suppliers.FirstOrDefaultAsync(_ => _.SupplierId == Id);

                if (supplier is not null)
                {
                    dbContext.Suppliers.Remove(supplier);
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(new Unit()) : new(new NotDeletedSupplierException(Id));
                }

                return new (new NotFoundSupplierException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> ExistsAsync(Guid Id)
        {
            try
            {
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.SupplierId == Id);

                return supplier is not null ? new(new Unit()) : new(new NotFoundSupplierException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<SupplierEntity>>> GetAllSuppliersAsync()
        {
            try
            {
                var suppliers = await dbContext.Suppliers
                    .AsNoTracking()
                    .ToListAsync();
                return new(suppliers);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierEntity>> GetSupplierByIdAsync(Guid Id)
        {
            try
            {
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.SupplierId == Id);

                return supplier is not null ? new(supplier) : new(new NotFoundSupplierException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierEntity>> GetSupplierByNameAsync(string Name)
        {
            try
            {
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.SupplierName == Name);

                return supplier is not null ? new(supplier) : new(new NotFoundSupplierException(Name));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierEntity>> UpdateSupplierAsync(SupplierEntity Supplier, Guid Id)
        {
            try
            {
                var oldEntity = await dbContext.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.SupplierId == Id);

                if (oldEntity is not null)
                {
                    Supplier.SupplierId = Id;

                    var newEntity = (dbContext.Suppliers.Update(Supplier)).Entity;
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(newEntity) : new(new NotUpdatedSupplierException());
                }

                return new(new NotFoundSupplierException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }
}
