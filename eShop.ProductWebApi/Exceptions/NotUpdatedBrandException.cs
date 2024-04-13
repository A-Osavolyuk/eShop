using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotUpdatedBrandException() : Exception("Cannot update brand due to server error."), IInternalServerError;
}
