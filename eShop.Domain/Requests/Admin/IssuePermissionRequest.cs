using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record class IssuePermissionRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public HashSet<string> Permissions { get; set; } = new HashSet<string>();
    }
}
