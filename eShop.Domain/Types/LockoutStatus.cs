namespace eShop.Domain.Types;

public class LockoutStatus
{
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}