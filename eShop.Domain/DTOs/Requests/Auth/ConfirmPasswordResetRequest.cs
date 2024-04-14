namespace eShop.Domain.DTOs.Requests.Auth
{
    public class ConfirmPasswordResetRequest
    {
        public string ResetToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
