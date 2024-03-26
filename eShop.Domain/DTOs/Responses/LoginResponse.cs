namespace eShop.Domain.DTOs.Responses;

public record LoginResponse(
    UserDto User,
    string Token = ""
    );
