using eShop.Domain.Interfaces;

namespace eShop.ReviewsWebApi.Exceptions
{
    public class DeniedUpdateException() : Exception($"Cannot update comment/review of another user"), IBadRequestException;
}
