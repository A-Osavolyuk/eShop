using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface IProductsService
    {
        public ValueTask<ResponseDto> GetAllProductsAsync();
        public ValueTask<ResponseDto> GetProductByIdAsync(Guid Id);
        public ValueTask<ResponseDto> GetProductByNameAsync(string Name);
        public ValueTask<ResponseDto> CreateProductAsync(ProductDto Product);
        public ValueTask<ResponseDto> UpdateProductAsync(ProductDto Product, Guid Id);
        public ValueTask<ResponseDto> DeleteProductByIdAsync(Guid Id);
    }
}
