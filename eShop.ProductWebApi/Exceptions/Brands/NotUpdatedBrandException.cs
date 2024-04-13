using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Brands
{
    public class NotUpdatedBrandException(Guid Id) : Exception($"Cannot update brand with id: {Id} due to server error."), IInternalServerError;
}
