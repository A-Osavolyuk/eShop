namespace eShop.Domain.Interfaces;

public interface ICacheService
{
    public ValueTask<bool> IsKeyExistsAsync(string key);
    public ValueTask<T> GetAsync<T>(string key);
    public ValueTask SetAsync<T>(string key, T value, TimeSpan? expirationTime = null);
    public ValueTask RemoveAsync(string key);
}