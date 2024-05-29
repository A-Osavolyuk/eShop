using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;
using eShop.ProductWebApi.Exceptions.Brands;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Repositories
{
    public class BrandsRepository(ProductDbContext context, IMapper mapper, ILogger<BrandsRepository> logger) : IBrandsRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<BrandsRepository> logger = logger;

        public async ValueTask<Result<BrandDTO>> CreateBrandAsync(Brand brand)
        {
            try
            {
                logger.LogInformation("Trying to create new brand.");
                var data = (await context.Brands.AddAsync(brand)).Entity;
                await context.SaveChangesAsync();
                var output = mapper.Map<BrandDTO>(data);

                logger.LogInformation("Brand was successfully created.");
                return new(output);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on creating brand with error message: {dbUpdateException.InnerException}");
                return new(new NotCreatedBrandException());
            }
            catch (Exception ex) 
            {
                logger.LogError($"Failed on creating brand with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteBrandAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete brand with id: {Id}.");
                var brand = await context.Brands.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == Id);

                if (brand is not null)
                {
                    context.Brands.Remove(brand);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Successfully deleted brand with id: {Id}.");
                    return new(new Unit());
                }

                return new(new NotFoundBrandException(Id));

            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on deleting brand with id: {Id} with error message: {dbUpdateException.InnerException}");
                return new(new NotDeletedBrandException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting brand with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<BrandDTO>> UpdateBrandAsync(Brand Brand)
        {
            try
            {
                logger.LogInformation($"Trying to update brand with id: {Brand.Id}.");
                var exists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == Brand.Id);

                if (exists)
                {
                    var data = context.Brands.Update(Brand).Entity;
                    await context.SaveChangesAsync();
                    var output = mapper.Map<BrandDTO>(data);

                    logger.LogInformation($"Successfully updated brand with id: {Brand.Id}.");
                    return new(output);
                }

                return new(new NotFoundBrandException(Brand.Id));
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on updating brand with id: {Brand.Id} with error message: {dbUpdateException.InnerException}");
                return new(new NotUpdatedBrandException(Brand.Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating brand with id: {Brand.Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<BrandDTO>> GetBrandByIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get brand with id: {Id}.");
                var brand = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);
                if (brand is not null)
                {
                    logger.LogInformation($"Successfully got brand with id: {Id}.");
                    return new(brand);
                }
                logger.LogInformation($"Cannot find brand wih id: {Id}");
                return new(new NotFoundBrandException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting brand with id: {Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<BrandDTO>> GetBrandByNameAsync(string Name)
        {
            try
            {
                logger.LogInformation($"Trying to get brand with name: {Name}.");
                var brand = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name);
                if (brand is not null)
                {
                    logger.LogInformation($"Successfully got brand with name: {Name}.");
                    return new(brand);
                }
                logger.LogInformation($"Cannot find brand wih name: {Name}");
                return new(new NotFoundBrandException(Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting brand with name: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<BrandDTO>>> GetBrandsListAsync()
        {
            try
            {
                logger.LogInformation($"Trying to get list of brands");
                var brands = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).ToListAsync();
                logger.LogInformation($"Successfully got list of brands");
                return new(brands);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting list od brands with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<string>>> GetBrandsNamesListAsync()
        {
            try
            {
                logger.LogInformation($"Trying to get list of brands");
                var brandsNames = await context.Brands.AsNoTracking().Select(_ => _.Name).ToListAsync();
                logger.LogInformation($"Successfully got list of brands");
                return new(brandsNames);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting list od brands with error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IBrandsRepository
    {
        public ValueTask<Result<IEnumerable<BrandDTO>>> GetBrandsListAsync();
        public ValueTask<Result<IEnumerable<string>>> GetBrandsNamesListAsync();
        public ValueTask<Result<BrandDTO>> GetBrandByIdAsync(Guid Id);
        public ValueTask<Result<BrandDTO>> GetBrandByNameAsync(string Name);
        public ValueTask<Result<BrandDTO>> CreateBrandAsync(Brand brand);
        public ValueTask<Result<Unit>> DeleteBrandAsync(Guid Id);
        public ValueTask<Result<BrandDTO>> UpdateBrandAsync(Brand brand);
    }
}
