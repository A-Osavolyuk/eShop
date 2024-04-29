using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using Unit = LanguageExt.Unit;

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
                        Article = x.Article,
                        Name = x.Name,
                        Description = x.Description,
                        ProductType = x.ProductType,
                        Price = new Money
                        {
                            Amount = x.Price.Amount,
                            Currency = x.Price.Currency,
                        },
                        Brand = new BrandDTO
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Country = x.Brand.Country
                        },
                        Supplier = new SupplierDTO
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
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                if (product is not null)
                {
                    var output = product.ProductType switch
                    {
                        ProductType.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id),
                        ProductType.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id),
                        _ => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id)
                    };

                    logger.LogInformation($"Successfully got product with id: {Id}.");
                    return new(output!);
                }

                var notFoundProductException = new NotFoundProductException(Id);
                logger.LogInformation($"Failed on getting product with id: {Id} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
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
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == Name);
                if (product is not null)
                {
                    var output = product.ProductType switch
                    {
                        ProductType.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name),
                        ProductType.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name),
                        _ => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name)
                    };

                    logger.LogInformation($"Successfully got product with name: {Name}.");
                    return new(output!);
                }

                var notFoundProductException = new NotFoundProductException(Name);
                logger.LogInformation($"Failed on getting product with name: {Name} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with name: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> CreateProductAsync(Product product)
        {
            var productTypeName = product.ProductType.ToString().ToLowerInvariant();
            try
            {
                logger.LogInformation($"Trying to create product of type {productTypeName}.");

                var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == product.BrandId);
                var supplierExists = await context.Suppliers.AsNoTracking().AnyAsync(_ => _.Id == product.SupplierId);

                if (bransExists)
                {
                    if (supplierExists)
                    {
                        product.Article = ArticleGenerator();

                        var entity = product.ProductType switch
                        {
                            ProductType.Clothing => (await context.Clothing.AddAsync(mapper.Map<Clothing>(product))).Entity,
                            ProductType.Shoes => (await context.Shoes.AddAsync(mapper.Map<Shoes>(product))).Entity,
                            _ => new Product()
                        };

                        await context.SaveChangesAsync();

                        logger.LogInformation($"Product of type: {productTypeName} was successfully created.");
                        return entity.ProductType switch
                        {
                            ProductType.Clothing => new(mapper.Map<ClothingDTO>(entity)),
                            ProductType.Shoes => new(mapper.Map<ShoesDTO>(entity)),
                            _ => new(mapper.Map<ProductDTO>(entity))
                        };
                    }

                    var notFoundSupplierException = new NotFoundSupplierException(product.SupplierId);
                    logger.LogWarning($"Failed on creating product of type: {productTypeName} with error message: {notFoundSupplierException.Message}.");
                    return new(notFoundSupplierException);
                }

                var notFoundBrandException = new NotFoundBrandException(product.BrandId);
                logger.LogWarning($"Failed on creating product of type: {productTypeName} with error message: {notFoundBrandException.Message}.");
                return new(notFoundBrandException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on creating product of type: {productTypeName} with error message: {dbUpdateException.InnerException}");
                return new(new NotCreatedProductException());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating product of type: {productTypeName} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> UpdateProductAsync(Product product)
        {
            var productTypeName = product.ProductType.ToString().ToLowerInvariant();
            try
            {
                logger.LogInformation($"Trying to update product of type {productTypeName}.");

                var productExists = await context.Products.AsNoTracking().AnyAsync(_ => _.Id == product.Id && _.ProductType == product.ProductType);
                if (productExists)
                {
                    var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == product.BrandId);
                    if (bransExists)
                    {
                        var supplierExists = await context.Suppliers.AsNoTracking().AnyAsync(_ => _.Id == product.SupplierId);
                        if (supplierExists)
                        {
                            var entity = product.ProductType switch
                            {
                                ProductType.Clothing => context.Clothing.Update(mapper.Map<Clothing>(product)).Entity,
                                ProductType.Shoes => context.Shoes.Update(mapper.Map<Shoes>(product)).Entity,
                                _ => new Product()
                            };

                            await context.SaveChangesAsync();

                            logger.LogInformation($"Product of type: {productTypeName} was successfully updated.");
                            return entity.ProductType switch
                            {
                                ProductType.Clothing => new(mapper.Map<ClothingDTO>(entity)),
                                ProductType.Shoes => new(mapper.Map<ShoesDTO>(entity)),
                                _ => new(mapper.Map<ProductDTO>(entity))
                            };
                        }

                        var notFoundSupplierException = new NotFoundSupplierException(product.SupplierId);
                        logger.LogWarning($"Failed on updating product of type: {productTypeName} with error message: {notFoundSupplierException.Message}.");
                        return new(notFoundSupplierException);
                    }

                    var notFoundBrandException = new NotFoundBrandException(product.BrandId);
                    logger.LogWarning($"Failed on updating product of type: {productTypeName} with error message: {notFoundBrandException.Message}.");
                    return new(notFoundBrandException);
                }
                var notFoundProductException = new NotFoundProductException(product.Id);
                logger.LogWarning($"Failed on updating product of type: {productTypeName} with error message: {notFoundProductException.Message}.");
                return new(notFoundProductException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on updating product of type: {productTypeName} with error message: {dbUpdateException.InnerException}");
                return new(new NotUpdatedProductException(product.Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating product of type: {productTypeName} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteProductByIdAsync(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete product with id: {Id}.");
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                if (product is not null)
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    logger.LogInformation($"Product with id: {Id} was successfully deleted.");
                    return new(new Unit());
                }
                var notFoundProductException = new NotFoundProductException(Id);
                logger.LogInformation($"Failed on deleting product with id: {Id} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on deleting product with error message: {dbUpdateException.InnerException}");
                return new(new NotDeletedProductException(Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting product with error message: {ex.Message}");
                return new(ex);
            }
        }

        private long ArticleGenerator()
        {
            var article = new Random().NextInt64(100_000_000, 100_000_000_000);
            return article;
        }

        public async ValueTask<Result<ProductDTO>> GetProductByArticleAsync(long Article)
        {
            try
            {
                logger.LogInformation($"Trying to get product with article: {Article}.");
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Article == Article);
                if (product is not null)
                {
                    var output = product.ProductType switch
                    {
                        ProductType.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article),
                        ProductType.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article),
                        _ => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article)
                    };

                    logger.LogInformation($"Successfully got product with article: {Article}.");
                    return new(output!);
                }

                var notFoundProductException = new NotFoundProductException(Article);
                logger.LogInformation($"Failed on getting product with article: {Article} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with article: {Article} error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync();
        public ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id);
        public ValueTask<Result<ProductDTO>> GetProductByArticleAsync(long Article);
        public ValueTask<Result<ProductDTO>> GetProductByNameAsync(string Name);
        public ValueTask<Result<ProductDTO>> CreateProductAsync(Product product);
        public ValueTask<Result<ProductDTO>> UpdateProductAsync(Product product);
        public ValueTask<Result<Unit>> DeleteProductByIdAsync(Guid Id);
    }
}
