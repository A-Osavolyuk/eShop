namespace eShop.Domain.Responses.Admin
{
    public class UserLockoutStatusResponse
    {
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
