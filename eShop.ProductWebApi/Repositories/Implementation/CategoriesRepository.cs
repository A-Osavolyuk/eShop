namespace eShop.ProductWebApi.Repositories.Implementation
{
    public class CategoriesRepository(ProductDbContext dbContext) : ICategoriesRepository
    {
        private readonly ProductDbContext dbContext = dbContext;

        public async ValueTask<Result<CategoryEntity>> CreateCategoryAsync(CategoryEntity category)
        {
            try
            {
                var entity = (await dbContext.Categories.AddAsync(category)).Entity;
                var creationResult = await dbContext.SaveChangesAsync();

                return creationResult > 0 ? new(entity) : new(new NotCreatedCategoryException());
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteCategoryByIdAsync(Guid id)
        {
            try
            {
                var category = await dbContext.Categories.FirstOrDefaultAsync(_ => _.CategoryId == id);

                if (category is not null)
                {
                    dbContext.Categories.Remove(category);
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(new Unit()) : new(new NotDeletedCategoryException(id));
                }

                return new(new NotFoundCategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<CategoryEntity>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await dbContext.Categories
                    .AsNoTracking()
                    .Include(category => category.Subcategories)
                    .Select(category => new CategoryEntity()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        Subcategories = category.Subcategories
                            .Select(subcategory => new SubcategoryEntity() 
                            { 
                                SubcategoryId = subcategory.SubcategoryId, 
                                SubcategoryName = subcategory.SubcategoryName,
                                CategoryId = subcategory.CategoryId,
                                Category = null!
                            }).ToList()
                    }).ToListAsync();

                return new(categories);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<CategoryEntity>> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var category = await dbContext.Categories
                    .AsNoTracking()
                    .Include(x => x.Subcategories)
                    .Select(category => new CategoryEntity()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        Subcategories = category.Subcategories
                            .Select(subcategory => new SubcategoryEntity()
                            {
                                SubcategoryId = subcategory.SubcategoryId,
                                SubcategoryName = subcategory.SubcategoryName,
                                CategoryId = subcategory.CategoryId,
                                Category = null!
                            }).ToList()
                    }).FirstOrDefaultAsync(_ => _.CategoryId == id);

                if (category is not null)
                    return new(category);

                return new(new NotFoundCategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<CategoryEntity>> GetCategoryByNameAsync(string name)
        {
            try
            {
                var category = await dbContext.Categories
                    .AsNoTracking()
                    .Include(x => x.Subcategories)
                    .Select(category => new CategoryEntity()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        Subcategories = category.Subcategories
                            .Select(subcategory => new SubcategoryEntity()
                            {
                                SubcategoryId = subcategory.SubcategoryId,
                                SubcategoryName = subcategory.SubcategoryName,
                                CategoryId = subcategory.CategoryId,
                                Category = null!
                            }).ToList()
                    }).FirstOrDefaultAsync(_ => _.CategoryName == name);

                if (category is not null)
                    return new(category);

                return new(new NotFoundCategoryException(name));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<CategoryEntity>> UpdateCategoryAsync(CategoryEntity newData, Guid id)
        {
            try
            {
                var category = await dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.CategoryId == id);

                if (category is not null)
                {
                    newData.CategoryId = id;
                    dbContext.Categories.Update(newData);
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(newData) : new(new NotUpdatedCategoryException());
                }

                return new(new NotFoundCategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> ExistsAsync(Guid id)
        {
            try
            {
                var category = await dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.CategoryId == id);

                if (category is not null)
                    return new(new Unit());

                return new(new NotFoundCategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }

        }
    }
}
