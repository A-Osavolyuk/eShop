using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using eShop.ProductWebApi.Exceptions.Brands;

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
                logger.LogInformation($"Cannot find product with id: {Id}");
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
                logger.LogInformation($"Cannot find product with name: {Name}");
                return new(new NotFoundProductException(Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with name: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> GetProductByIdWithTypeAsync(Guid Id, ProductType Type)
        {
            try
            {
                logger.LogInformation($"Trying to get product of type {Type.ToString()} by id: {Id}.");

                var product = Type switch
                {
                    ProductType.Clothing => await context.Products.AsNoTracking().OfType<ClothingDTO>().FirstOrDefaultAsync(x => x.Id == Id),
                    ProductType.Shoes => await context.Products.AsNoTracking().OfType<ShoesDTO>().FirstOrDefaultAsync(x => x.Id == Id),
                    _ => await context.Products.AsNoTracking().OfType<ProductDTO>().FirstOrDefaultAsync(x => x.Id == Id)
                };

                if (product is not null)
                {
                    logger.LogInformation($"Successfully got product with type {Type.ToString()} by id: {Id}.");
                    return new(product);
                }
                logger.LogInformation($"Cannot find product with id: {Id}");
                return new(new NotFoundProductException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with type: {Type.ToString()} by id: {Id} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> GetProductByNameWithTypeAsync(string Name, ProductType Type)
        {
            try
            {
                logger.LogInformation($"Trying to get product of type {Type.ToString()} by id: {Name}.");

                var product = Type switch
                {
                    ProductType.Clothing => await context.Products.AsNoTracking().OfType<ClothingDTO>().FirstOrDefaultAsync(x => x.Name == Name),
                    ProductType.Shoes => await context.Products.AsNoTracking().OfType<ShoesDTO>().FirstOrDefaultAsync(x => x.Name == Name),
                    _ => await context.Products.AsNoTracking().OfType<ProductDTO>().FirstOrDefaultAsync(x => x.Name == Name)
                };

                if (product is not null)
                {
                    logger.LogInformation($"Successfully got product with type {Type.ToString()} by id: {Name}.");
                    return new(product);
                }
                logger.LogInformation($"Cannot find product with id: {Name}");
                return new(new NotFoundProductException(Name));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with type: {Type.ToString()} by id: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> CreateProductAsync(Product product)
        {
            try
            {
                logger.LogInformation($"Trying to create product of type {product.ProductType.ToString().ToLowerInvariant()}.");

                var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == product.BrandId);
                var supplierExists = await context.Suppliers.AsNoTracking().AnyAsync(_ => _.Id == product.SupplierId);

                if (bransExists)
                {
                    if (supplierExists)
                    {
                        var entity = product.ProductType switch
                        {
                            ProductType.Clothing => (await context.Clothing.AddAsync(mapper.Map<Clothing>(product))).Entity,
                            ProductType.Shoes => (await context.Shoes.AddAsync(mapper.Map<Shoes>(product))).Entity,
                            _ => new Product()
                        };

                        await context.SaveChangesAsync();

                        logger.LogInformation($"Product of type: {product.ProductType.ToString().ToLowerInvariant()} was successfully created.");
                        return entity.ProductType switch
                        {
                            ProductType.Clothing => new(mapper.Map<ClothingDTO>(entity)),
                            ProductType.Shoes => new(mapper.Map<ShoesDTO>(entity)),
                            _ => new(mapper.Map<ProductDTO>(entity))
                        };
                    }

                    var notFoundSupplierException = new NotFoundSupplierException(product.SupplierId);
                    logger.LogWarning($"Failed on creating product of type: {product.ProductType.ToString().ToLowerInvariant()} with error message: {notFoundSupplierException.Message}.");
                    return new(notFoundSupplierException);
                }

                var notFoundBrandException = new NotFoundBrandException(product.BrandId);
                logger.LogWarning($"Failed on creating product of type: {product.ProductType.ToString().ToLowerInvariant()} with error message: {notFoundBrandException.Message}.");
                return new(notFoundBrandException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on creating product of type: {product.ProductType.ToString().ToLowerInvariant()} with error message: {dbUpdateException.InnerException}");
                return new(new NotCreateProductException());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating product of type: {product.ProductType.ToString().ToLowerInvariant()} with error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync();
        public ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id);
        public ValueTask<Result<ProductDTO>> GetProductByNameAsync(string Name);
        public ValueTask<Result<ProductDTO>> GetProductByIdWithTypeAsync(Guid Id, ProductType Type);
        public ValueTask<Result<ProductDTO>> GetProductByNameWithTypeAsync(string Name, ProductType Type);
        public ValueTask<Result<ProductDTO>> CreateProductAsync(Product product);
    }
}
