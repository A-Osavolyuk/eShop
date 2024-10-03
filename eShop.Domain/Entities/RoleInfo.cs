namespace eShop.Domain.Entities
{
    public class RoleInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
    }
}
