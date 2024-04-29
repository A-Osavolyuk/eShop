using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;

namespace eShop.Domain.Interfaces
{
    public interface IProductService
    {
        public ValueTask<ResponseDTO> GetProductsListAsync();
        public ValueTask<ResponseDTO> GetProductByIdAsync(Guid Id);
        public ValueTask<ResponseDTO> GetProductByArticleAsync(long Article);
        public ValueTask<ResponseDTO> GetProductByNameAsync(string Name);
        public ValueTask<ResponseDTO> GetProductByIdWithTypeAsync(Guid Id, ProductType Type);
        public ValueTask<ResponseDTO> GetProductByNameWithTypeAsync(string Name, ProductType Type);
        public ValueTask<ResponseDTO> CreateProductAsync(CreateClothingRequest request);
        public ValueTask<ResponseDTO> CreateProductAsync(CreateShoesRequest request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateClothingRequest request);
        public ValueTask<ResponseDTO> UpdateProductAsync(UpdateShoesRequest request);
        public ValueTask<ResponseDTO> DeleteProductAsync(Guid Id);
    }
}
