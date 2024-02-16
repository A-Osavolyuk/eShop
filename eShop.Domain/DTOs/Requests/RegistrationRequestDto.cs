namespace eShop.Domain.DTOs.Requests;

public record RegistrationRequestDto(
    string Email = "", 
    string Name = "", 
    string Password = "", 
    string ConfirmPassword = "");
