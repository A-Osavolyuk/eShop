﻿namespace eShop.Domain.Responses.Auth
{
    public class ChangeUserNameResponse
    {
        public string Message { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
