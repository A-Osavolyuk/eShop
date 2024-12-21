namespace eShop.Domain.Entities.AuthApi;

public class CodeEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public string UserId { get; init; } = string.Empty;
    public int Code { get; init; }
    public CodeType CodeType { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(10);
}