using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Subcategories
{
    public class NotDeletedSubcategoryException(Guid id) : Exception($"Subcategory with id: {id} was not deleted due to DB error."), IInternalServerError;
}
