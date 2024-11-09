using eShop.Domain.DTOs;
using eShop.Domain.Requests.Product;

namespace eShop.Domain.Interfaces
{
    public interface IProductService
    {
        public ValueTask<ResponseDto> CreateProductAsync(CreateProductRequest request);
        public ValueTask<ResponseDto> UpdateProductAsync(UpdateProductRequest request);
        public ValueTask<ResponseDto> DeleteProductAsync(DeleteProductRequest request);
        public ValueTask<ResponseDto> GetProductsAsync();
        public ValueTask<ResponseDto> GetProductByNameAsync(string name);
        public ValueTask<ResponseDto> GetProductByArticleAsync(string article);
        public ValueTask<ResponseDto> GetProductByIdAsync(Guid id);
    }
}
