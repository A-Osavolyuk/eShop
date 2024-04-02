using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Subcategories
{
    public class NotCreatedSubcategoryException() : Exception("Subcategory was not created due to DB error."), IInternalServerError;
}
