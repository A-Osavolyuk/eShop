namespace eShop.Domain.Requests.Admin
{
    public record class RemoveUserRolesRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
