namespace eShop.Domain.DTOs.Responses
{
    public class ResetPasswordResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
