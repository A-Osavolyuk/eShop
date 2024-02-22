namespace eShop.Domain.Exceptions
{
    public class NotDeletedCategoryException(Guid id) : Exception($"Category with id: {id} was not deleted due to DB error.");
}
