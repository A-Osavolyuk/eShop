using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Brands
{
    public class NotUpdatedBrandException() : Exception("Cannot update brand due to server error."), IInternalServerError;
}
