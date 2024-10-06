using Microsoft.EntityFrameworkCore;

namespace eShop.Domain.Entities.Admin
{
    public class UserPermissions
    {
        public string UserId { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }

        public AppUser User { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}
