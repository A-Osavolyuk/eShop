namespace eShop.Domain.Entities.AuthApi;

public class UserAuthenticationTokenEntity
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiredAt { get; set; } = DateTime.UtcNow.AddDays(30);

    public AppUser User { get; set; } = null!;
}