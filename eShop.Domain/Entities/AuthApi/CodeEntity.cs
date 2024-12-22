namespace eShop.Domain.Entities.AuthApi;

public class CodeEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public string SentTo { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public CodeType CodeType { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddMinutes(10);
}