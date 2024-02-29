namespace eShop.Domain.Exceptions
{
    public class NotDeletedSubcategoryException(Guid id) : Exception($"Subcategory with id: {id} was not deleted due to DB error.");
}
