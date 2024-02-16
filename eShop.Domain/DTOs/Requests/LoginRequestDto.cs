namespace eShop.Domain.DTOs.Requests;

public record LoginRequestDto(
    string Email = "",
    string Password = "");
