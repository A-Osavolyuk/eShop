using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using LanguageExt.Common;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public ValueTask<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest);

        public ValueTask<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest);

        public ValueTask<Result<ChangePersonalDataResponseDto>> ChangePersonalDataAsync(string UserId, ChangePersonalDataRequestDto changePersonalDataRequest);

        public ValueTask<Result<PersonalDataDto>> GetPersonalDataAsync(string UserId);

        public ValueTask<Result<ChangePasswordResponseDto>> ChangePassword(string UserId, ChangePasswordRequestDto changePasswordRequest);

        public ValueTask<Result<ResetPasswordResponseDto>> ResetPasswordRequest(string UserEmail);
        public ValueTask<Result<ConfirmPasswordResetResponseDto>> ConfirmResetPassword(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest);
    }
}
