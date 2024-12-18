namespace eShop.Infrastructure.Services;

public class CookieManager(IHttpContextAccessor contextAccessor, IJSRuntime js) : ICookieManager
{
    private readonly IHttpContextAccessor contextAccessor = contextAccessor;
    private readonly IJSRuntime js = js;

    public void SetCookie(string name, string value, CookieOptions options)
    {
        contextAccessor.HttpContext?.Response.Cookies.Append(name, value, options);
    }

    public async Task SetCookie<T>(string name, T value, int days)
    {
        var data = JsonConvert.SerializeObject(value);
        await js.InvokeVoidAsync("setCookie", name, data, days);
    }

    public async Task<T> GetCookie<T>(string name)
    {
        return await js.InvokeAsync<T>("getCookie", name);
    }

    public async Task DeleteCookie(string name)
    {
        await js.InvokeVoidAsync("deleteCookie", name);
    }
}