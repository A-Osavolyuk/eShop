namespace eShop.ProductWebApi.Repositories.Interfaces
{
    public interface ISubcategoriesRepository
    {
        public ValueTask<Result<IEnumerable<SubcategoryEntity>>> GetAllCategoriesAsync();
        public ValueTask<Result<SubcategoryEntity>> GetSubcategoryByIdAsync(Guid id);
        public ValueTask<Result<SubcategoryEntity>> GetSubcategoryByNameAsync(string name);
        public ValueTask<Result<SubcategoryEntity>> CreateSubcategoryAsync(SubcategoryEntity Subcategory);
        public ValueTask<Result<SubcategoryEntity>> UpdateSubcategoryAsync(SubcategoryEntity Subcategory, Guid id);
        public ValueTask<Result<Unit>> DeleteSubcategoryByIdAsync(Guid id);
        public ValueTask<Result<Unit>> Exists(Guid id);
    }
}
