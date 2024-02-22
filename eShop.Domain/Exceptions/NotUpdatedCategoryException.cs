namespace eShop.Domain.Exceptions
{
    public class NotUpdatedCategoryException() : Exception("Category was not updated due to DB error.");
}
