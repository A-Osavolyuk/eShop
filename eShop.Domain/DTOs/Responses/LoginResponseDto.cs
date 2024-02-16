namespace eShop.Domain.DTOs.Responses;

public record LoginResponseDto(
    UserDto User,
    string Token = ""
    );
