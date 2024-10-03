namespace eShop.Domain.Entities.Admin
{
    public class LockoutStatus
    {
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
