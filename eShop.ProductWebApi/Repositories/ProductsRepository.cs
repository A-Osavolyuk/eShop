namespace eShop.ProductWebApi.Repositories
{
    public class ProductsRepository(ProductDbContext context) : IProductRepository
    {
        private readonly ProductDbContext context = context;

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsList()
        {
            try
            {
                return await context.Products
                    .AsNoTracking()
                    .Select(x => new ProductDTO()
                    {
                        Id = x.Id,
                        Brand = x.Brand,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        ProductType = x.ProductType,
                        Supplier = x.Supplier,
                    })
                    .ToListAsync();
            }
            catch (Exception ex) 
            {
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsList();
    }
}
