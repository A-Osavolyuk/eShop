using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;
using eShop.ProductWebApi.Exceptions.Suppliers;
using Microsoft.Extensions.Logging;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Repositories
{
    public class SuppliersRepository(ProductDbContext context, IMapper mapper, ILogger<SuppliersRepository> logger) : ISuppliersRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<SuppliersRepository> logger = logger;

        public async ValueTask<Result<SupplierDTO>> CreateSupplierAsync(Supplier supplier)
        {
            try
            {
                logger.LogInformation("Trying to create new supplier.");
                var data = (await context.Suppliers.AddAsync(supplier)).Entity;
                await context.SaveChangesAsync();
                var output = mapper.Map<SupplierDTO>(data);

                logger.LogInformation("Supplier was successfully created.");
                return new(output);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on creating supplier with error message: {dbUpdateException.InnerException}");
                return new(new NotCreatedSupplierException());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating supplier with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteSupplierAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete supplier with id: {Id}.");
                var supplier = await context.Suppliers.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == Id);

                if (supplier is not null)
                {
                    context.Suppliers.Remove(supplier);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Successfully deleted supplier with id: {Id}.");
                    return new(new Unit());
                }

                return new(new NotFoundSupplierException(Id));

            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on deleting supplier with id: {Id} with error message: {dbUpdateException.InnerException}");
                return new(new NotDeletedSupplierException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting supplier with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierDTO>> UpdateSupplierAsync(Supplier Supplier)
        {
            try
            {
                logger.LogInformation($"Trying to update supplier with id: {Supplier.Id}.");
                var exists = await context.Suppliers.AsNoTracking().AnyAsync(_ => _.Id == Supplier.Id);

                if (exists)
                {
                    var data = context.Suppliers.Update(Supplier).Entity;
                    await context.SaveChangesAsync();
                    var output = mapper.Map<SupplierDTO>(data);

                    logger.LogInformation($"Successfully updated supplier with id: {Supplier.Id}.");
                    return new(output);
                }

                return new(new NotFoundSupplierException(Supplier.Id));
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on updating supplier with id: {Supplier.Id} with error message: {dbUpdateException.InnerException}");
                return new(new NotUpdatedSupplierException(Supplier.Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating supplier with id: {Supplier.Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierDTO>> GetSupplierByIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get supplier with id: {Id}.");
                var supplier = await context.Suppliers.AsNoTracking().ProjectTo<SupplierDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);
                if (supplier is not null)
                {
                    logger.LogInformation($"Successfully got supplier with id: {Id}.");
                    return new(supplier);
                }
                logger.LogInformation($"Cannot find supplier wih id: {Id}");
                return new(new NotFoundSupplierException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting supplier with id: {Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<SupplierDTO>> GetSupplierByNameAsync(string Name)
        {
            try
            {
                logger.LogInformation($"Trying to get supplier with name: {Name}.");
                var supplier = await context.Suppliers.AsNoTracking().ProjectTo<SupplierDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name);
                if (supplier is not null)
                {
                    logger.LogInformation($"Successfully got supplier with name: {Name}.");
                    return new(supplier);
                }
                logger.LogInformation($"Cannot find supplier wih name: {Name}");
                return new(new NotFoundSupplierException(Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting supplier with name: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<SupplierDTO>>> GetSuppliersListAsync()
        {
            try
            {
                logger.LogInformation($"Trying to get list of suppliers");
                var suppliers = await context.Suppliers.AsNoTracking().ProjectTo<SupplierDTO>(mapper.ConfigurationProvider).ToListAsync();
                logger.LogInformation($"Successfully got list of suppliers");
                return new(suppliers);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting list od suppliers with error message: {ex.Message}");
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
