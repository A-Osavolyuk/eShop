namespace eShop.Domain.DTOs.Responses
{
    public class ChangePersonalDataResponseDto
    {
        public string Token { get; set; } = "";
        public UserDto User { get; set; } = null!;
    }
}
