﻿namespace eShop.Domain.DTOs.Responses
{
    public class ResetPasswordResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public string ResetToken { get; set; } = string.Empty;
    }
}