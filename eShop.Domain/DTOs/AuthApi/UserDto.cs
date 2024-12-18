namespace eShop.Domain.DTOs.AuthApi;

public record UserDto(
    string Email = "",
    string UserName = "",
    string Id = ""
    );
