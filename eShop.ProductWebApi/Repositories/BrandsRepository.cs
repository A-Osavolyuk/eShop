using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.ProductWebApi.Exceptions;

namespace eShop.ProductWebApi.Repositories
{
    public class BrandsRepository(ProductDbContext context, IMapper mapper) : IBrandsRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<BrandDTO>> CreateBrandAsync(Brand brand)
        {
            try
            {
                var data = (await context.Brands.AddAsync(brand)).Entity;
                var result = await context.SaveChangesAsync();
                var output = mapper.Map<BrandDTO>(data);

                return result > 0 ? new(output) : new(new NotCreateBrandException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<BrandDTO>> GetBrandByIdAsync(Guid Id)
        {
            try
            {
                var brand = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);
                return brand is null ? new(new NotFoundBrandException(Id)) : new(brand);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<BrandDTO>> GetBrandByNameAsync(string Name)
        {
            try
            {
                var brand = await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name);
                return brand is null ? new(new NotFoundBrandException(Name)) : new(brand);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<BrandDTO>>> GetBrandsListAsync()
        {
            try
            {
                return await context.Brands.AsNoTracking().ProjectTo<BrandDTO>(mapper.ConfigurationProvider).ToListAsync();
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }

    public interface IBrandsRepository
    {
        public ValueTask<Result<IEnumerable<BrandDTO>>> GetBrandsListAsync();
        public ValueTask<Result<BrandDTO>> GetBrandByIdAsync(Guid Id);
        public ValueTask<Result<BrandDTO>> GetBrandByNameAsync(string Name);
        public ValueTask<Result<BrandDTO>> CreateBrandAsync(Brand brand);
    }
}
