namespace eShop.Domain.Requests.Admin
{
    public record class RemoveUserFromPermissionRequest
    {
        public Guid UserId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
    }
}
