﻿namespace eShop.Domain.DTOs.Responses.Auth;

public record LoginResponse
{
    public UserDTO User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool HasTwoFactorAuthentication { get; set; } = false;
}