using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;

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
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request, Guid Id);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request, Guid Id);
        public ValueTask<ResponseDTO> DeleteProductAsync(Guid Id);
        public ValueTask<ResponseDTO> SearchProductAsync(long Article);
        public ValueTask<ResponseDTO> SearchProductAsync(string Name);
    }
}
