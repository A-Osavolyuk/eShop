using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Categories
{
    public class NotUpdatedCategoryException() : Exception("Category was not updated due to DB error."), IInternalServerError;
}
