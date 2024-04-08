namespace eShop.ProductWebApi.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        public ValueTask<Result<IEnumerable<ProductEntity>>> GetAllProductsAsync();
        public ValueTask<Result<ProductEntity>> GetProductByIdAsync(Guid Id);
        public ValueTask<Result<ProductEntity>> GetProductByNameAsync(string Name);
        public ValueTask<Result<ProductEntity>> CreateProductAsync(ProductEntity product);
        public ValueTask<Result<ProductEntity>> UpdateProductAsync(ProductEntity product, Guid Id);
        public ValueTask<Result<Unit>> DeleteProductByIdAsync(Guid Id);
        public ValueTask<Result<Unit>> ExistsAsync( Guid Id);
    }
}
