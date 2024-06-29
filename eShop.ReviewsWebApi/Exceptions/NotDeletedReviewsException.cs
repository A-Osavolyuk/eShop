using eShop.Domain.Interfaces;

namespace eShop.ReviewsWebApi.Exceptions
{
    public class NotDeletedReviewsException(Guid ProductId) : Exception($"Cannot delete reviews with product id: {ProductId} due to server error"), IInternalServerError; 
}
