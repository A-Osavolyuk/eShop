namespace eShop.Domain.Messages
{
    public class ReviewsDeletedMessage
    {
        public bool IsSucceeded { get; set; } = false;
        public string Status { get; set; } = string.Empty;
    }
}
