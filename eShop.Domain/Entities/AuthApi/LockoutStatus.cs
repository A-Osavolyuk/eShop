namespace eShop.Domain.Entities.AuthApi;

public class LockoutStatus
{
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}