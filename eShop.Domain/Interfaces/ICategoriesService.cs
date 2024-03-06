using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface ICategoriesService
    {
        public ValueTask<ResponseDto> GetAllCategoriesAsync();
        public ValueTask<ResponseDto> GetCategoryByIdAsync(Guid Id);
        public ValueTask<ResponseDto> GetCategoryByNameAsync(string Name);
        public ValueTask<ResponseDto> CreateCategoryAsync(CategoryDto Category);
        public ValueTask<ResponseDto> UpdateCategoryAsync(CategoryDto Category, Guid Id);
        public ValueTask<ResponseDto> DeleteCategoryByIdAsync(Guid Id);
    }
}
