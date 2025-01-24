﻿namespace eShop.Domain.Requests.Api.Auth;

public record class VerifyEmailRequest
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}