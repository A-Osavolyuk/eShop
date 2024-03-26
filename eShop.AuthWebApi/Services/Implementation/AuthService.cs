using LanguageExt.Pretty;
using OpenTelemetry.Trace;
using System.Net;

namespace eShop.AuthWebApi.Services.Implementation
{
    public partial class AuthService(
        ITokenHandler tokenHandler,
        UserManager<AppUser> userManager,
        IValidator<RegistrationRequestDto> registrationValidator,
        IValidator<LoginRequestDto> loginValidator,
        IValidator<ChangePersonalDataRequestDto> personalDataValidator,
        IValidator<ChangePasswordRequestDto> passwordValidator,
        IValidator<ConfirmPasswordResetRequest> resetPasswordValidator,
        IMapper mapper,
        IBus bus) : IAuthService
    {
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IValidator<RegistrationRequestDto> registrationValidator = registrationValidator;
        private readonly IValidator<LoginRequestDto> loginValidator = loginValidator;
        private readonly IValidator<ChangePersonalDataRequestDto> personalDataValidator = personalDataValidator;
        private readonly IValidator<ChangePasswordRequestDto> passwordValidator = passwordValidator;
        private readonly IValidator<ConfirmPasswordResetRequest> resetPasswordValidator = resetPasswordValidator;
        private readonly IMapper mapper = mapper;
        private readonly IBus bus = bus;

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

        public async ValueTask<Result<ConfirmPasswordResetResponseDto>> ConfirmResetPassword(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var validationResult = await resetPasswordValidator.ValidateAsync(confirmPasswordResetRequest);

                    if (validationResult.IsValid)
                    {
                        var token = new StringBuilder(confirmPasswordResetRequest.ResetToken).Replace(" ", "+").ToString();
                        var resetResult = await userManager.ResetPasswordAsync(user, token, confirmPasswordResetRequest.NewPassword);

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

                var user = await userManager.FindByEmailAsync(loginRequest.Email);

                if (user is not null)
                {
                    var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                    if (isValidPassword)
                    {
                        var token = tokenHandler.GenerateToken(user);
                        var userDto = new UserDto(user.Email!, user.Email!, user.Id);

                        return new(new LoginResponseDto(userDto, token));
                    }

                    return new(new InvalidLoginAttemptException());
                }

                return new(new NotFoundUserByEmailException(loginRequest.Email));
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
                    var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var link = UrlGenerator.ActionLink("confirm-email", "account",
                        new { Email = registrationRequest.Email, Token = emailConfirmationToken }, "https", new HostString("localhost", 5102));

                    var endpoint = await bus.GetSendEndpoint(new Uri("rabbitmq://localhost/confirm-email"));

                    await endpoint.Send(new ConfirmEmailRequest()
                    {
                        Link = link,
                        To = registrationRequest.Email,
                        Subject = "Reset password request",
                        UserName = $"{(!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)
                            ? $"{user.FirstName + " " + user.LastName}" : user.Email)}"
                    });

                    return new(new RegistrationResponseDto() { Message = $"We have sent an email with instructions to your email address." });
                }

                return new(new InvalidRegisterAttemptException("Invalid registration attempt.",
                    registrationResult.Errors.Select(x => x.Description)));
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
                    var link = UrlGenerator.ActionLink("confirm-password-reset", "account",
                        new { Email = UserEmail, Token = token }, "https", new HostString("localhost", 5102));

                    var uri = new Uri("rabbitmq://localhost/reset-password");
                    var endpoint = await bus.GetSendEndpoint(uri);
                    await endpoint.Send(new SendResetPasswordEmailRequest()
                    {
                        Link = link,
                        To = UserEmail,
                        Subject = "Reset password request",
                        UserName = $"{(!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)
                            ? $"{user.FirstName + " " + user.LastName}" : user.Email)}"
                    });

                    return new(new ResetPasswordResponseDto()
                    {
                        Message = $"We have sent an email with instructions to your email address."
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
