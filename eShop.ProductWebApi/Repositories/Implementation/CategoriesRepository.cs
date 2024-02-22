using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Data;
using eShop.ProductWebApi.Repositories.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace eShop.ProductWebApi.Repositories.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ProductDbContext dbContext;

        public CategoriesRepository(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async ValueTask<Result<CategoryEntity>> CreateCategoryAsync(CategoryEntity category)
        {
            try
            {
                var entity = await dbContext.Categories.AddAsync(category);
                var creationResult = await dbContext.SaveChangesAsync();

                if (creationResult > 0)
                    return new Result<CategoryEntity>(entity.Entity);

                return new Result<CategoryEntity>(new NotCreatedProductCategoryException());
            }
            catch (Exception ex)
            {
                return new Result<CategoryEntity>(ex);
            }
        }

        public ValueTask<Result<bool>> DeleteCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Result<IEnumerable<CategoryEntity>>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Result<CategoryEntity>> GetCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Result<CategoryEntity>> GetCategoryByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Result<CategoryEntity>> UpdateCategoryAsync(CategoryEntity category, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
