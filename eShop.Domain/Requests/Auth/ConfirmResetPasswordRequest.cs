﻿namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class ConfirmResetPasswordRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
        public string ResetToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}