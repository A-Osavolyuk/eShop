namespace eShop.Domain.Requests.AuthApi.Admin;

public record LockoutUserRequest
{
    public Guid UserId { get; set; }
    public DateTimeOffset LockoutEnd { get; set; }
    public bool Permanent { get; set; }
}