﻿namespace eShop.Domain.Requests.AuthApi.Auth;

public record class RegistrationRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}