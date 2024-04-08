using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Categories
{
    public class NotCreatedCategoryException() : Exception("Category was not created due to DB error."), IInternalServerError;
}
