using AutoMapper;
using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Exceptions.Auth;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHandler tokenHandler;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IValidator<RegistrationRequestDto> registrationValidator;
        private readonly IValidator<LoginRequestDto> loginValidator;
        private readonly IValidator<ChangePersonalDataRequestDto> personalDataValidator;
        private readonly IValidator<ChangePasswordRequestDto> passwordValidator;
        private readonly IValidator<ConfirmPasswordResetRequestDto> resetPasswordValidator;
        private readonly IMapper mapper;

        public AuthService(
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IValidator<RegistrationRequestDto> registrationValidator,
            IValidator<LoginRequestDto> loginValidator,
            IValidator<ChangePersonalDataRequestDto> personalDataValidator,
            IValidator<ChangePasswordRequestDto> passwordValidator,
            IValidator<ConfirmPasswordResetRequestDto> resetPasswordValidator,
            IMapper mapper)
        {
            this.tokenHandler = tokenHandler;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.registrationValidator = registrationValidator;
            this.loginValidator = loginValidator;
            this.personalDataValidator = personalDataValidator;
            this.passwordValidator = passwordValidator;
            this.resetPasswordValidator = resetPasswordValidator;
            this.mapper = mapper;
        }

        public async ValueTask<Result<ChangePasswordResponseDto>> ChangePassword(string UserId, ChangePasswordRequestDto changePasswordRequest)
        {
            try
            {
                var user = await userManager.FindByIdAsync(UserId);

                if (user is not null)
                {
                    var validationResult = await passwordValidator.ValidateAsync(changePasswordRequest);

                    if (validationResult.IsValid)
                    {
                        var isCorrectPassword = await userManager.CheckPasswordAsync(user, changePasswordRequest.OldPassword);

                        if (isCorrectPassword)
                        {
                            var result = await userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

                            if (result.Succeeded)
                            {
                                return new(new ChangePasswordResponseDto() { Message = "Password has been successfully changed." });
                            }

                            return new(new NotChangedPasswordException(result.Errors.First().Description));
                        }

                        return new(new WrongPasswordException());
                    }

                    return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
                }

                return new(new NotFoundUserByIdException(UserId));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangePersonalDataResponseDto>> ChangePersonalDataAsync(string Id, ChangePersonalDataRequestDto changePersonalDataRequest)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);

                if (user is not null)
                {
                    var validationResult = await personalDataValidator.ValidateAsync(changePersonalDataRequest);

                    if (validationResult.IsValid)
                    {
                        user.AddPersonalData(changePersonalDataRequest);

                        var result = await userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return new(new ChangePersonalDataResponseDto()
                            {
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                MiddleName = user.MiddleName,
                                Gender = user.Gender,
                                DateOfBirth = user.DateOfBirth,
                            });
                        }

                        return new(new NotChangedPersonalDataException());
                    }

                    return new(new FailedValidationException("Validation Error(s)", validationResult.Errors.Select(x => x.ErrorMessage)));
                }

                return new(new NotFoundUserByIdException(Id));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ConfirmPasswordResetResponseDto>> ConfirmResetPassword(string Email, ConfirmPasswordResetRequestDto confirmPasswordResetRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var validationResult = await resetPasswordValidator.ValidateAsync(confirmPasswordResetRequest);

                    if (validationResult.IsValid)
                    {
                        var resetResult = await userManager.ResetPasswordAsync(user, confirmPasswordResetRequest.ResetToken, confirmPasswordResetRequest.NewPassword);

                        if (resetResult.Succeeded)
                        {
                            return new(new ConfirmPasswordResetResponseDto() { Message = "Your password has been successfully reset." });
                        }

                        return new(new NotResetPasswordException());
                    }

                    return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<PersonalDataDto>> GetPersonalDataAsync(string Id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);

                if (user is null)
                    return new(new NotFoundUserByIdException(Id));

                return new(new PersonalDataDto()
                {
                    DateOfBirth = user.DateOfBirth,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    MiddleName = user.MiddleName,
                    PhoneNumber = user.PhoneNumber ?? ""
                });
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            try
            {
                var validationResult = await loginValidator.ValidateAsync(loginRequest);

                if (loginRequest is null)
                    return new(new NullRequestException(type: loginRequest!.GetType()));

                if (!validationResult.IsValid)
                    return new(new FailedValidationException("Validation Error(s)",
                        validationResult.Errors.Select(x => x.ErrorMessage)));

                var loginResult = await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

                if (loginResult.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(loginRequest.Email);
                    var token = tokenHandler.GenerateToken(user);
                    var userDto = new UserDto(user.Email!, user.Email!, user.Id);

                    return new(new LoginResponseDto(userDto, token));
                }

                return new(new InvalidLoginAttemptException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest)
        {
            try
            {
                var validationResult = await registrationValidator.ValidateAsync(registrationRequest);

                if (registrationRequest is null)
                    return new(new NullRequestException(type: registrationRequest!.GetType()));

                if (!validationResult.IsValid)
                    return new(new FailedValidationException("Validation Error(s)",
                        validationResult.Errors.Select(x => x.ErrorMessage)));

                var user = mapper.Map<AppUser>(registrationRequest);
                var registrationResult = await userManager.CreateAsync(user, registrationRequest.Password);

                if (registrationResult.Succeeded)
                {
                    return new(
                        new RegistrationResponseDto($"Successfully registered."));
                }

                return new(
                    new InvalidRegisterAttemptException("Invalid registration attempt.", registrationResult.Errors.Select(x => x.Description)));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ResetPasswordResponseDto>> ResetPasswordRequest(string UserEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(UserEmail);

                if (user is not null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    return new(new ResetPasswordResponseDto()
                    {
                        Message = $"We have sent a mail with instruction to your email address.",
                        ResetToken = token
                    });
                }

                return new(new NotFoundUserByEmailException(UserEmail));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }
}
