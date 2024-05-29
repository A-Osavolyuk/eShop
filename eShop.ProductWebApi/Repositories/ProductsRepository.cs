using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;
using LanguageExt;
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
                        Compound = x.Compound,
                        Amount = x.Price,
                        Currency = x.Currency,
                        Brand = new BrandDTO
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Country = x.Brand.Country
                        }
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

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsByNameAsync(string Name)
        {
            try
            {
                logger.LogInformation($"Trying to get products with name: {Name}.");
                var list = await context.Products
                    .AsNoTracking()
                    .Where(x => x.Name.Contains(Name))
                    .Select(x => new ProductDTO()
                    {
                        Id = x.Id,
                        Article = x.Article,
                        Name = x.Name,
                        Description = x.Description,
                        ProductType = x.ProductType,
                        Compound = x.Compound,
                        Amount = x.Price,
                        Currency = x.Currency,
                        Brand = new BrandDTO
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Country = x.Brand.Country
                        }
                    })
                    .ToListAsync();

                logger.LogInformation($"Successfully got products with name: {Name}.");
                return new(list);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting products with name: {Name} with error message: {ex.Message}.");
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
                        Categoty.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id),
                        Categoty.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id),
                        _ or Categoty.None => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id)
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
                        Categoty.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article),
                        Categoty.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article),
                        _ or Categoty.None => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Article == Article)
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
                        Categoty.Clothing => await context.Products.AsNoTracking().OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name),
                        Categoty.Shoes => await context.Products.AsNoTracking().OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name),
                        _ or Categoty.None => await context.Products.AsNoTracking().OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == Name)
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

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsByVariantIdAsync(Guid VariantId)
        {
            try
            {
                logger.LogInformation($"Trying to get product with variant id: {VariantId}.");
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.VariantId == VariantId);
                if (product is not null)
                {
                    var output = product.ProductType switch
                    {
                        Categoty.Clothing => context.Products.AsNoTracking().Where(x => x.VariantId == VariantId).OfType<Clothing>().ProjectTo<ClothingDTO>(mapper.ConfigurationProvider),
                        Categoty.Shoes => context.Products.AsNoTracking().Where(x => x.VariantId == VariantId).OfType<Shoes>().ProjectTo<ShoesDTO>(mapper.ConfigurationProvider),
                        Categoty.None or _ => context.Products.AsNoTracking().Where(x => x.VariantId == VariantId).OfType<Product>().ProjectTo<ProductDTO>(mapper.ConfigurationProvider)
                    };

                    logger.LogInformation($"Successfully got product with variant id: {VariantId}.");
                    return new(await output.ToListAsync()!);
                }

                var notFoundProductGroupException = new NotFoundProductGroupException(VariantId);
                logger.LogInformation($"Failed on getting product with variant id: {VariantId} error message: {notFoundProductGroupException.Message}");
                return new(notFoundProductGroupException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting product with variant id: {VariantId} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<TResponse>>> CreateProductAsync<TResponse, TRequest>(IEnumerable<TRequest> request)
            where TResponse : ProductDTO
            where TRequest : Product
        {
            try
            {
                logger.LogInformation($"Trying to create product.");

                var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == request.First().BrandId);

                if (bransExists)
                {
                    logger.LogInformation($"Creating a variety of products.");

                    await context.Products.AddRangeAsync(request);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Products were successfully created.");

                    return new(await context.Products
                        .AsNoTracking()
                        .OfType<TRequest>()
                        .ProjectTo<TResponse>(mapper.ConfigurationProvider)
                        .ToListAsync());
                }

                var notFoundBrandException = new NotFoundBrandException(request.First().BrandId);
                logger.LogWarning($"Failed on creating product with error message: {notFoundBrandException.Message}.");
                return new(notFoundBrandException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on creating product with error message: {dbUpdateException.InnerException}");
                return new(new NotCreatedProductException());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on creating product with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<TResponse>> UpdateProductAsync<TResponse, TEntity, TRequest>(TRequest request)
            where TEntity : Product
            where TResponse : ProductDTO
            where TRequest : UpdateProductRequestBase
        {
            try
            {
                logger.LogInformation($"Trying to update product.");

                var productExists = await context.Products.AsNoTracking().AnyAsync(_ => _.Id == request.Id && _.ProductType == request.ProductType);
                if (productExists)
                {
                    var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == request.BrandId);
                    if (bransExists)
                    {
                        logger.LogInformation($"Updating product.");

                        var product = mapper.Map<TEntity>(request);
                        var entity = context.Products.Update(product).Entity;
                        await context.SaveChangesAsync();

                        logger.LogInformation($"Product was successfully updated.");

                        return new(mapper.Map<TResponse>(entity));
                    }

                    var notFoundBrandException = new NotFoundBrandException(request.BrandId);
                    logger.LogWarning($"Failed on updating product with error message: {notFoundBrandException.Message}.");
                    return new(notFoundBrandException);
                }
                var notFoundProductException = new NotFoundProductException(request.Id);
                logger.LogWarning($"Failed on updating product with error message: {notFoundProductException.Message}.");
                return new(notFoundProductException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogError($"Failed on updating product with error message: {dbUpdateException.InnerException}");
                return new(new NotUpdatedProductException(request.Id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on updating product with error message: {ex.Message}");
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

        public async ValueTask<Result<SearchProductResponse>> SearchAsync(long Article)
        {
            try
            {
                logger.LogInformation($"Trying to search product with article: {Article}.");
                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Article == Article);
                if (product is not null)
                {
                    logger.LogInformation($"Successfully found product with article: {Article}.");
                    return new(new SearchProductResponse() { Found = true, Count = 1 });
                }

                var notFoundProductException = new NotFoundProductException(Article);
                logger.LogInformation($"Failed on searching product with article: {Article} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on searching product with article: {Article} error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<SearchProductResponse>> SearchAsync(string Name)
        {
            try
            {
                logger.LogInformation($"Trying to search product(s) with name contains: {Name}.");
                var quantity = await context.Products.AsNoTracking().Where(x => x.Name.Contains(Name)).CountAsync();
                if (quantity > 0)
                {
                    logger.LogInformation($"Successfully found product(s) with name contains: {Name} in quantity: {quantity}.");
                    return new(new SearchProductResponse() { Found = true, Count = quantity });
                }

                var notFoundProductException = new NotFoundProductException(Name);
                logger.LogInformation($"Failed on searching product with name contains: {Name} error message: {notFoundProductException.Message}");
                return new(notFoundProductException);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on searching product with name contains: {Name} error message: {ex.Message}");
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync();
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsByNameAsync(string Name);
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsByVariantIdAsync(Guid VariantId);
        public ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id);
        public ValueTask<Result<ProductDTO>> GetProductByArticleAsync(long Article);
        public ValueTask<Result<ProductDTO>> GetProductByNameAsync(string Name);
        public ValueTask<Result<IEnumerable<TResponse>>> CreateProductAsync<TResponse, TRequest>(IEnumerable<TRequest> request)
            where TResponse : ProductDTO where TRequest : Product;
        public ValueTask<Result<TResponse>> UpdateProductAsync<TResponse, TEntity, TRequest>(TRequest request)
            where TEntity : Product where TResponse : ProductDTO where TRequest : UpdateProductRequestBase;
        public ValueTask<Result<Unit>> DeleteProductByIdAsync(Guid Id);
        public ValueTask<Result<SearchProductResponse>> SearchAsync(long Article);
        public ValueTask<Result<SearchProductResponse>> SearchAsync(string Name);
    }
}
