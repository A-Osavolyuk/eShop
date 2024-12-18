﻿namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ExternalLoginRequest
{
    public string Provider { get; set; } = string.Empty;
    public string ReturnUri { get; set; } = string.Empty;
}