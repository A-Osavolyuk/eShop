﻿namespace eShop.Domain.Messages.Email;

public class ChangeEmailMessage : EmailBase
{
    public string Code { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty;
}