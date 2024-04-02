using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Products
{
    public class NotDeletedProductException(Guid id) : Exception($"Product with id: {id} was not deleted due to DB error."), IInternalServerError;
}
