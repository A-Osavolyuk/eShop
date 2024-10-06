namespace eShop.Domain.Entities.Admin
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<UserPermissions> Permissions { get; set; } = null!;
    }
}
