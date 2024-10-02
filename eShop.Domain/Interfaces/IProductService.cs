using eShop.Domain.DTOs;
using eShop.Domain.Requests.Product;

namespace eShop.Domain.Interfaces
{
    public interface IProductService
    {
        public ValueTask<ResponseDTO> GetProductsListAsync();
        public ValueTask<ResponseDTO> GetProductsByNameAsync(string Name);
        public ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id);
        public ValueTask<ResponseDTO> GetProductByArticleAsync(long Article);
        public ValueTask<ResponseDTO> GetProductByNameAsync(string Name);
        public ValueTask<ResponseDTO> CreateProductAsync(IEnumerable<CreateProductRequest> request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateProductRequest request, Guid Id);
        public ValueTask<ResponseDTO> DeleteProductAsync(DeleteProductRequest request);
        public ValueTask<ResponseDTO> SearchProductAsync(long Article);
        public ValueTask<ResponseDTO> SearchProductAsync(string Name);
    }
}
