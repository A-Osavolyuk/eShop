using eShop.Domain.DTOs;
using eShop.Domain.Requests.Product;

namespace eShop.Domain.Interfaces
{
    public interface IProductService
    {
        public ValueTask<ResponseDTO> CreateProductAsync(CreateProductRequest request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateProductRequest request);
        public ValueTask<ResponseDTO> DeleteProductAsync(DeleteProductRequest request);
        public ValueTask<ResponseDTO> GetProductsAsync();
        public ValueTask<ResponseDTO> GetProductByNameAsync(string name);
        public ValueTask<ResponseDTO> GetProductByArticleAsync(string article);
        public ValueTask<ResponseDTO> GetProductByIdAsync(Guid id);
    }
}
