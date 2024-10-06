using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record class RemoveUserFromPermissionRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
    }
}
