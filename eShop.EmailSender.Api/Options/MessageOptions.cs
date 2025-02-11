﻿namespace eShop.EmailSender.Api.Options;

public class MessageOptions
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}