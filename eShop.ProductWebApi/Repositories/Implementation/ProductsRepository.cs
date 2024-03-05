
namespace eShop.ProductWebApi.Repositories.Implementation
{
    public class ProductsRepository(ProductDbContext dbContext) : IProductsRepository
    {
        private readonly ProductDbContext dbContext = dbContext;

        public async ValueTask<Result<ProductEntity>> CreateProductAsync(ProductEntity product)
        {
            try
            {
                var entity = (await dbContext.Products.AddAsync(product)).Entity;
                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? new(entity) : new(new NotCreatedProductException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteProductByIdAsync(Guid Id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(_ => _.ProductId == Id);

                if (product is not null)
                {
                    dbContext.Products.Remove(product);
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(new Unit()) : new(new NotDeletedProductException(Id));
                }

                return new(new NotFoundProductException(Id));
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
                var product = await dbContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ =>  _.ProductId == Id);

                return product is not null ? new(new Unit()) : new(new NotFoundProductException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<ProductEntity>>> GetAllProductsAsync()
        {
            try
            {
                var products = await dbContext.Products
                    .AsNoTracking()
                    .Include(product => product.Supplier)
                    .Include(product => product.Subcategory)
                    .Select(product => new ProductEntity()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        ProductDescription = product.ProductDescription,
                        SubcategoryId = product.SubcategoryId,
                        SupplierId = product.SupplierId,
                        Supplier = product.Supplier,
                        Subcategory = new SubcategoryEntity()
                        {
                            SubcategoryId = product.Subcategory.SubcategoryId,
                            Name = product.Subcategory.Name,
                            CategoryId = product.Subcategory.CategoryId,
                            Category = null!
                        }
                    })
                    .ToListAsync();

                return new(products);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductEntity>> GetProductByIdAsync(Guid Id)
        {
            try
            {
                var product = await dbContext.Products
                    .AsNoTracking()
                    .Include(product => product.Supplier)
                    .Include(product => product.Subcategory)
                    .Select(product => new ProductEntity()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        ProductDescription = product.ProductDescription,
                        SubcategoryId = product.SubcategoryId,
                        SupplierId = product.SupplierId,
                        Supplier = product.Supplier,
                        Subcategory = new SubcategoryEntity()
                        {
                            SubcategoryId = product.Subcategory.SubcategoryId,
                            Name = product.Subcategory.Name,
                            CategoryId = product.Subcategory.CategoryId,
                            Category = null!
                        }
                    })
                    .FirstOrDefaultAsync(_ => _.ProductId == Id);

                return product is not null ? new(product) : new(new NotFoundProductException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductEntity>> GetProductByNameAsync(string Name)
        {
            try
            {
                var product = await dbContext.Products
                    .AsNoTracking()
                    .Include(product => product.Supplier)
                    .Include(product => product.Subcategory)
                    .Select(product => new ProductEntity()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        ProductDescription = product.ProductDescription,
                        SubcategoryId = product.SubcategoryId,
                        SupplierId = product.SupplierId,
                        Supplier = product.Supplier,
                        Subcategory = new SubcategoryEntity()
                        {
                            SubcategoryId = product.Subcategory.SubcategoryId,
                            Name = product.Subcategory.Name,
                            CategoryId = product.Subcategory.CategoryId,
                            Category = null!
                        }
                    })
                    .FirstOrDefaultAsync(_ => _.Name == Name);

                return product is not null ? new(product) : new(new NotFoundProductException(Name));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductEntity>> UpdateProductAsync(ProductEntity NewProduct, Guid Id)
        {
            try
            {
                var oldProduct = await dbContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.ProductId == Id);

                if (oldProduct is not null)
                {
                    NewProduct.ProductId = Id;
                    var entity = (dbContext.Products.Update(NewProduct)).Entity;
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(entity) : new(new NotUpdatedProductException());
                }

                return new(new NotFoundProductException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }
}
