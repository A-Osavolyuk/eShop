namespace eShop.Domain.Responses.Admin
{
    public class LockoutStatusResponse
    {
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
