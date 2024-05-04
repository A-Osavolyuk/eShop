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
        public ValueTask<ResponseDTO> CreateProductAsync(CreateClothingRequest request);
        public ValueTask<ResponseDTO> CreateProductAsync(CreateShoesRequest request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request);
        public ValueTask<ResponseDTO> DeleteProductAsync(Guid Id);
        public ValueTask<ResponseDTO> SearchProductAsync(long Article);
        public ValueTask<ResponseDTO> SearchProductAsync(string Name);
    }
}
