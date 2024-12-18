namespace eShop.Domain.Requests.Admin
{
    public record class DeleteUserAccountRequest
    {
        public Guid UserId { get; set; }
    }
}
