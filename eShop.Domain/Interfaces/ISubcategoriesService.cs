using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface ISubcategoriesService
    {
        public ValueTask<ResponseDto> GetAllSubcategoriesAsync();
        public ValueTask<ResponseDto> GetSubcategoryByIdAsync(Guid Id);
        public ValueTask<ResponseDto> GetSubcategoryByNameAsync(string Name);
        public ValueTask<ResponseDto> CreateSubcategoryAsync(SubcategoryDto Subcategory);
        public ValueTask<ResponseDto> UpdateSubcategoryAsync(SubcategoryDto Subcategory, Guid Id);
        public ValueTask<ResponseDto> DeleteSubcategoryByIdAsync(Guid Id);
    }
}
