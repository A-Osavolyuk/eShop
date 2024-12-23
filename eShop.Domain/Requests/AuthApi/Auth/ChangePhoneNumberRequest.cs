﻿namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ChangePhoneNumberRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}