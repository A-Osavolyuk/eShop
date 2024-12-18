namespace eShop.Domain.Requests.Admin
{
    public record RemoveUserRoleRequest
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
