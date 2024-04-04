namespace eShop.Domain.DTOs;

public record UserDto(
    string Email = "",
    string UserName = "",
    string Id = ""
    );
