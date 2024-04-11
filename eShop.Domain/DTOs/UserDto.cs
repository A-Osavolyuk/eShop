namespace eShop.Domain.DTOs;

public record UserDTO(
    string Email = "",
    string UserName = "",
    string Id = ""
    );
