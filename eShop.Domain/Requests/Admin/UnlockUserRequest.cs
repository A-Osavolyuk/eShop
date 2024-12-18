namespace eShop.Domain.Requests.Admin
{
    public record UnlockUserRequest
    {
        public Guid UserId { get; set; }
    }
}
