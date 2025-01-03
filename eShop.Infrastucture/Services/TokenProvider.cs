﻿namespace eShop.Infrastructure.Services;

public class TokenProvider : ITokenProvider
{
    private readonly ILocalStorageService localStorage;

    public TokenProvider(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public async ValueTask<string> GetTokenAsync()
    {
        var token = await localStorage.GetItemAsStringAsync("jwt-access-token");

        if (!string.IsNullOrEmpty(token))
        {
            return token;
        }

        return string.Empty;
    }

    public async ValueTask ClearAsync()
    {
        await localStorage.RemoveItemAsync("jwt-access-token");
    }

    public async ValueTask SetTokenAsync(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            await localStorage.SetItemAsStringAsync("jwt-access-token", token);
        }
    }


}