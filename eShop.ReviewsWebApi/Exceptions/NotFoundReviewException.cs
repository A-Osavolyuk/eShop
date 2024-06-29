using eShop.Domain.Interfaces;

namespace eShop.ReviewsWebApi.Exceptions
{
    public class NotFoundReviewException(Guid Id) : Exception($"Cannot find review with product id: {Id}"), INotFoundException;
}
