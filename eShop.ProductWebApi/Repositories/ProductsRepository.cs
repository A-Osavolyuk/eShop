using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;

namespace eShop.ProductWebApi.Repositories
{
    public class ProductsRepository(ProductDbContext context, IMapper mapper, ILogger<ProductsRepository> logger) : IProductRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<IProductRepository> logger = logger;

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync()
        {
            try
            {
                logger.LogInformation($"Trying to get list of products.");
                var list = await context.Products
                    .AsNoTracking()
                    .Select(x => new ProductDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        ProductType = x.ProductType,
                        Price = new Money
                        {
                            Amount = x.Price.Amount,
                            Currency = x.Price.Currency,
                        },
                        Brand = new Brand
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Country = x.Brand.Country
                        },
                        Supplier = new Supplier
                        {
                            Id = x.Supplier.Id,
                            Name = x.Supplier.Name,
                            ContactEmail = x.Supplier.ContactEmail,
                            ContactPhone = x.Supplier.ContactPhone
                        },
                    })
                    .ToListAsync();

                logger.LogInformation($"Successfully got list of products.");
                return new(list);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting list of products with error message: {ex.Message}.");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get product with id: {Id}.");
                var product = await context.Products.AsNoTracking().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);
                if (product is not null)
                {
                    logger.LogInformation($"Successfully got product with id: {Id}.");
                    return new(product);
                }
                logger.LogInformation($"Cannot find product wih id: {Id}");
                return new(new NotFoundProductException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with id: {Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> GetProductByNameAsync(string Name)
        {
            try
            {
                logger.LogInformation($"Trying to get product with name: {Name}.");
                var product = await context.Products.AsNoTracking().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name);
                if (product is not null)
                {
                    logger.LogInformation($"Successfully got product with name: {Name}.");
                    return new(product);
                }
                logger.LogInformation($"Cannot find product wih name: {Name}");
                return new(new NotFoundProductException(Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with name: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync();
        public ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id);
        public ValueTask<Result<ProductDTO>> GetProductByNameAsync(string Name);
    }
}
