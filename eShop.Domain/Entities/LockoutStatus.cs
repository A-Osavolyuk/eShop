namespace eShop.Domain.Entities
{
    public class LockoutStatus
    {
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
