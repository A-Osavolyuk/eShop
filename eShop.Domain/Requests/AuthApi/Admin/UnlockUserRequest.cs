namespace eShop.Domain.Requests.AuthApi.Admin;

public record UnlockUserRequest
{
    public Guid UserId { get; set; }
}