
namespace eShop.ProductWebApi.Repositories.Implementation
{
    public class SubcategoriesRepository(ProductDbContext dbContext) : ISubcategoriesRepository
    {
        private readonly ProductDbContext dbContext = dbContext;

        public async ValueTask<Result<SubcategoryEntity>> CreateSubcategoryAsync(SubcategoryEntity Subcategory)
        {
            try
            {
                var category = await dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.CategoryId == Subcategory.CategoryId);

                if (category is not null)
                {
                    var entity = (await dbContext.Subcategories.AddAsync(Subcategory)).Entity;
                    var creationResult = await dbContext.SaveChangesAsync();

                    return creationResult > 0 ? new(entity) : new(new NotCreatedSubcategoryException());
                }

                return new(new NotFoundCategoryException(Subcategory.CategoryId));
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteSubcategoryByIdAsync(Guid id)
        {
            try
            {
                var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(_ => _.SubcategoryId == id);

                if (subcategory is not null)
                {
                    dbContext.Subcategories.Remove(subcategory);
                    var result = await dbContext.SaveChangesAsync();

                    return result > 0 ? new(new Unit()) : new(new NotDeletedSubcategoryException(id));
                }

                return new(new NotFoundSubcategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> Exists(Guid id)
        {
            try
            {
                var category = await dbContext.Subcategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.CategoryId == id);

                if (category is not null)
                    return new(new Unit());

                return new(new NotFoundSubcategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<SubcategoryEntity>>> GetAllSubcategoriesAsync()
        {
            try
            {
                var subcategories = await dbContext.Subcategories
                    .AsNoTracking()
                    .Include(subcategory => subcategory.Category)
                    .Select(subcategory => new SubcategoryEntity()
                    {
                        SubcategoryId = subcategory.SubcategoryId,
                        Name = subcategory.Name,
                        CategoryId = subcategory.CategoryId,
                        Category = new CategoryEntity()
                        {
                            CategoryId = subcategory.Category.CategoryId,   
                            Name = subcategory.Category.Name,
                            Subcategories = null!
                        }
                    })
                    .ToListAsync();

                return subcategories;
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SubcategoryEntity>> GetSubcategoryByIdAsync(Guid id)
        {
            try
            {
                var subcategory = await dbContext.Subcategories
                    .AsNoTracking()
                    .Include(subcategory => subcategory.Category)
                    .Select(subcategory => new SubcategoryEntity()
                    {
                        SubcategoryId = subcategory.SubcategoryId,
                        Name = subcategory.Name,
                        CategoryId = subcategory.CategoryId,
                        Category = new CategoryEntity()
                        {
                            CategoryId = subcategory.Category.CategoryId,
                            Name = subcategory.Category.Name,
                            Subcategories = null!
                        }
                    }).FirstOrDefaultAsync(subcategory => subcategory.SubcategoryId == id);

                return subcategory is not null ? new(subcategory) : new(new NotFoundSubcategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SubcategoryEntity>> GetSubcategoryByNameAsync(string name)
        {
            try
            {
                var subcategory = await dbContext.Subcategories
                    .AsNoTracking()
                    .Include(subcategory => subcategory.Category)
                    .Select(subcategory => new SubcategoryEntity()
                    {
                        SubcategoryId = subcategory.SubcategoryId,
                        Name = subcategory.Name,
                        CategoryId = subcategory.CategoryId,
                        Category = new CategoryEntity()
                        {
                            CategoryId = subcategory.Category.CategoryId,
                            Name = subcategory.Category.Name,
                            Subcategories = null!
                        }
                    }).FirstOrDefaultAsync(subcategory => subcategory.Name == name);

                return subcategory is not null ? new(subcategory) : new(new NotFoundSubcategoryException(name));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<SubcategoryEntity>> UpdateSubcategoryAsync(SubcategoryEntity newData, Guid id)
        {
            try
            {
                var subcategory = await dbContext.Subcategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.SubcategoryId == id);

                if (subcategory is not null)
                {
                    var category = await dbContext.Categories
                        .AsNoTracking()
                        .FirstOrDefaultAsync(_ => _.CategoryId == newData.CategoryId);

                    if (category is not null)
                    {
                        newData.SubcategoryId = id;
                        dbContext.Subcategories.Update(newData);
                        var result = await dbContext.SaveChangesAsync();

                        return result > 0 ? new(newData) : new(new NotUpdatedSubcategoryException());
                    }

                    return new(new NotFoundCategoryException(newData.CategoryId));
                }

                return new(new NotFoundSubcategoryException(id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }
}
