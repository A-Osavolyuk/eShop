using Microsoft.AspNetCore.Http;

namespace eShop.Domain.Interfaces
{
    public interface ICookieManager
    {
        public Task SetCookie<T>(string name, T value, int days);
        public Task<T> GetCookie<T>(string name);
        public Task DeleteCookie(string name);
    }
}
