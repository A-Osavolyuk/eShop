using eShop.Domain.Entities;
using LanguageExt.Common;

namespace eShop.ProductWebApi.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public ValueTask<Result<IEnumerable<CategoryEntity>>> GetAllCategoriesAsync();
        public ValueTask<Result<CategoryEntity>> GetCategoryByIdAsync(Guid id);
        public ValueTask<Result<CategoryEntity>> GetCategoryByNameAsync(string name);
        public ValueTask<Result<CategoryEntity>> CreateCategoryAsync(CategoryEntity category);
        public ValueTask<Result<CategoryEntity>> UpdateCategoryAsync(CategoryEntity category, Guid id);
        public ValueTask<Result<bool>> DeleteCategoryByIdAsync(Guid id);
        public ValueTask<bool> ExistsAsync(Guid id);
    }
}
