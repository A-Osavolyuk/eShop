﻿namespace eShop.Domain.Requests.AuthApi.Auth;

public class VerifyPhoneNumberRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}