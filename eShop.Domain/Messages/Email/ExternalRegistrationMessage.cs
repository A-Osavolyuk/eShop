﻿namespace eShop.Domain.Messages.Email;

public class ExternalRegistrationMessage : EmailBase
{
    public string TempPassword { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
}