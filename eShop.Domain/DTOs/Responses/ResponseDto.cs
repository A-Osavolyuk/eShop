namespace eShop.Domain.DTOs.Responses;

public record ResponseDto(
    object? Result,
    string Message = "",
    bool IsSucceeded = false);
