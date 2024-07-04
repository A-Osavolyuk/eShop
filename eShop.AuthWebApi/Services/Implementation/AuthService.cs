using eShop.AuthWebApi.Utilities;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using LanguageExt;
using System.Net;

namespace eShop.AuthWebApi.Services.Implementation
{
    public partial class AuthService(
        ITokenHandler tokenHandler,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IValidator<RegistrationRequest> registrationValidator,
        IValidator<LoginRequest> loginValidator,
        IValidator<ChangePersonalDataRequest> personalDataValidator,
        IValidator<ChangePasswordRequest> passwordValidator,
        IValidator<ConfirmPasswordResetRequest> resetPasswordValidator,
        IValidator<ChangeUserNameRequest> userNameValidator,
        IValidator<ChangeEmailRequest> emailValidator,
        IValidator<ChangePhoneNumberRequest> phoneValidator,
        IMapper mapper,
        IEmailSender emailSender,
        IConfiguration configuration) : IAuthService
    {
        private readonly ITokenHandler tokenHandler = tokenHandler;
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly SignInManager<AppUser> signInManager = signInManager;
        private readonly IValidator<RegistrationRequest> registrationValidator = registrationValidator;
        private readonly IValidator<LoginRequest> loginValidator = loginValidator;
        private readonly IValidator<ChangePersonalDataRequest> personalDataValidator = personalDataValidator;
        private readonly IValidator<ChangePasswordRequest> passwordValidator = passwordValidator;
        private readonly IValidator<ConfirmPasswordResetRequest> resetPasswordValidator = resetPasswordValidator;
        private readonly IValidator<ChangeUserNameRequest> userNameValidator = userNameValidator;
        private readonly IValidator<ChangeEmailRequest> emailValidator = emailValidator;
        private readonly IValidator<ChangePhoneNumberRequest> phoneValidator = phoneValidator;
        private readonly IMapper mapper = mapper;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async ValueTask<Result<ChangePasswordResponse>> ChangePasswordAsync(string Email, ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

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
                                return new(new ChangePasswordResponse() { Message = "Password has been successfully changed." });
                            }

                            return new(new NotChangedPasswordException(result.Errors.First().Description));
                        }

                        return new(new WrongPasswordException());
                    }

                    return new(new FailedValidationException(validationResult.Errors));
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(string Email, ChangePersonalDataRequest changePersonalDataRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var validationResult = await personalDataValidator.ValidateAsync(changePersonalDataRequest);

                    if (validationResult.IsValid)
                    {
                        user.AddPersonalData(changePersonalDataRequest);

                        var result = await userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return new(new ChangePersonalDataResponse()
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Gender = user.Gender,
                                DateOfBirth = user.DateOfBirth,
                            });
                        }

                        return new(new NotChangedPersonalDataException());
                    }

                    return new(new FailedValidationException(validationResult.Errors));
                }

                return new(new NotFoundUserByIdException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var token = Uri.UnescapeDataString(confirmEmailRequest.Token);
                    var confirmResult = await userManager.ConfirmEmailAsync(user, token);

                    if (confirmResult.Succeeded)
                    {
                        await emailSender.SendAccountRegisteredMessage(new AccountRegisteredMessage()
                        {
                            To = Email,
                            Subject = "Successful Account Registration",
                            UserName = user.UserName!
                        });

                        return new(new Unit());
                    }

                    return new(new NotConfirmedEmailException());
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ConfirmPasswordResetResponse>> ConfirmResetPasswordAsync(string Email, ConfirmPasswordResetRequest confirmPasswordResetRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var validationResult = await resetPasswordValidator.ValidateAsync(confirmPasswordResetRequest);

                    if (validationResult.IsValid)
                    {
                        var token = Uri.UnescapeDataString(confirmPasswordResetRequest.ResetToken);
                        var resetResult = await userManager.ResetPasswordAsync(user, token, confirmPasswordResetRequest.NewPassword);

                        if (resetResult.Succeeded)
                        {
                            return new(new ConfirmPasswordResetResponse() { Message = "Your password has been successfully reset." });
                        }

                        return new(new NotResetPasswordException());
                    }

                    return new(new FailedValidationException(validationResult.Errors));
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<PersonalDataResponse>> GetPersonalDataAsync(string Email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is null)
                    return new(new NotFoundUserByEmailException(Email));

                return new(new PersonalDataResponse()
                {
                    DateOfBirth = user.DateOfBirth,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                });
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<PhoneNumberResponse>> GetPhoneNumber(string Email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if(user is not null)
                {
                    return new(new PhoneNumberResponse()
                    {
                        PhoneNumber = user.PhoneNumber!
                    });
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var validationResult = await loginValidator.ValidateAsync(loginRequest);

                if (!validationResult.IsValid)
                    return new(new FailedValidationException(validationResult.Errors));

                var user = await userManager.FindByEmailAsync(loginRequest.Email);

                if (user is not null)
                {
                    if (user.EmailConfirmed)
                    {
                        var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                        if (isValidPassword)
                        {
                            var userDto = new UserDTO(user.Email!, user.UserName!, user.Id);

                            if (user.TwoFactorEnabled)
                            {
                                var loginCode = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                                await emailSender.SendTwoFactorAuthenticationCodeMessage(new TwoFactorAuthenticationCodeMessage()
                                {
                                    To = user.Email!,
                                    Subject = "Login with 2FA code",
                                    UserName = user.UserName!,
                                    Code = loginCode
                                });

                                return new(new LoginResponse()
                                {
                                    User = userDto,
                                    Message = "We have sent an email with 2FA code at your email address.",
                                    HasTwoFactorAuthentication = true
                                });
                            }

                            var token = tokenHandler.GenerateToken(user);

                            return new(new LoginResponse()
                            {
                                User = userDto,
                                Token = token,
                                Message = "Successfully logged in.",
                                HasTwoFactorAuthentication = false
                            });
                        }

                        return new(new InvalidLoginAttemptException());
                    }

                    return new(new InvalidLoginAttemptWithNotConfirmedEmailException());
                }

                return new(new NotFoundUserByEmailException(loginRequest.Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest)
        {
            try
            {
                var validationResult = await registrationValidator.ValidateAsync(registrationRequest);

                if (!validationResult.IsValid)
                    return new(new FailedValidationException(validationResult.Errors));

                var exists = await userManager.FindByEmailAsync(registrationRequest.Email);

                if (exists is null)
                {
                    var user = mapper.Map<AppUser>(registrationRequest);
                    var registrationResult = await userManager.CreateAsync(user, registrationRequest.Password);

                    if (registrationResult.Succeeded)
                    {
                        var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var encodedToken = Uri.EscapeDataString(emailConfirmationToken);

                        var link = UrlGenerator.ActionLink("/account/confirm-email", frontendUri,
                            new { Email = registrationRequest.Email, Token = encodedToken });

                        await emailSender.SendConfirmEmailMessage(new ConfirmEmailMessage()
                        {
                            To = registrationRequest.Email,
                            Subject = "Email Confirmation",
                            Link = link,
                            UserName = user.UserName!
                        });

                        return new(new RegistrationResponse()
                        {
                            Message = $"Your account have been successfully registered. " +
                            $"Now you have to confirm you email address to log in. " +
                            $"We have sent an email with instructions to your email address."
                        });
                    }

                    return new(new InvalidRegisterAttemptException());
                }

                return new(new UserAlreadyExistsException(registrationRequest.Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ResetPasswordResponse>> RequestResetPasswordAsync(string UserEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(UserEmail);

                if (user is not null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = Uri.EscapeDataString(token);
                    var link = UrlGenerator.ActionLink("/account/confirm-password-reset", frontendUri, new { Email = UserEmail, Token = encodedToken });

                    await emailSender.SendResetPasswordMessage(new ResetPasswordMessage()
                    {
                        To = UserEmail,
                        Subject = "Reset Password Request",
                        Link = link,
                        UserName = user.UserName!
                    });

                    return new(new ResetPasswordResponse()
                    {
                        Message = $"You have to confirm password reset. " +
                        $"We have sent an email with instructions to your email address."
                    });
                }

                return new(new NotFoundUserByEmailException(UserEmail));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangeTwoFactorAuthenticationResponse>> ChangeTwoFactorAuthenticationStateAsync(string Email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    IdentityResult result = null!;

                    if (user.TwoFactorEnabled)
                    {
                        result = await userManager.SetTwoFactorEnabledAsync(user, false);

                        if (result.Succeeded)
                        {
                            return new(new ChangeTwoFactorAuthenticationResponse()
                            {
                                Message = "Two factor authentication was successfully disabled.",
                                TwoFactorAuthenticationState = false
                            });
                        }

                        return new(new NotChangedTwoFactorAuthenticationException());
                    }
                    else
                    {
                        result = await userManager.SetTwoFactorEnabledAsync(user, true);

                        if (result.Succeeded)
                        {
                            return new(new ChangeTwoFactorAuthenticationResponse()
                            {
                                Message = "Two factor authentication was successfully enabled.",
                                TwoFactorAuthenticationState = false
                            });
                        }

                        return new(new NotChangedTwoFactorAuthenticationException());
                    }
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<TwoFactorAuthenticationStateResponse>> GetTwoFactorAuthenticationStateAsync(string Email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    return user.TwoFactorEnabled
                        ? new(new TwoFactorAuthenticationStateResponse() { TwoFactorAuthenticationState = true })
                        : new(new TwoFactorAuthenticationStateResponse() { TwoFactorAuthenticationState = false });
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<LoginResponse>> LoginWithTwoFactorAuthenticationCodeAsync(string Email,
            TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var result = await userManager.VerifyTwoFactorTokenAsync(user, "Email", twoFactorAuthenticationLoginRequest.Code);

                    if (result)
                    {
                        var userDto = new UserDTO(user.Email!, user.UserName!, user.Id);
                        var token = tokenHandler.GenerateToken(user);

                        return new(new LoginResponse()
                        {
                            User = userDto,
                            Token = token,
                            Message = "Successfully logged in."
                        });
                    }

                    return new(new InvalidTwoFactorAuthenticationCodeException());
                }

                return new(new NotFoundUserByEmailException(Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ExternalLoginResponse>> RequestExternalLogin(string provider, string? returnUri = null)
        {
            try
            {
                var providers = await signInManager.GetExternalAuthenticationSchemesAsync();

                var validProvider = providers.Any(x => x.DisplayName == provider);

                if (validProvider)
                {
                    var handlerUri = UrlGenerator.Action("handle-external-login-response", "Auth", new { ReturnUri = returnUri ?? "/" });
                    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, handlerUri);

                    return new(new ExternalLoginResponse()
                    {
                        Provider = provider,
                        AuthenticationProperties = properties
                    });
                }

                return new(new InvalidExternalProvider(provider));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<string>> HandleExternalLoginResponseAsync(ExternalLoginInfo externalLoginInfo, string ReturnUri)
        {
            try
            {
                var email = externalLoginInfo.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;

                if (email is not null)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    var token = string.Empty;

                    if (user is not null)
                    {
                        token = tokenHandler.GenerateToken(user);
                        var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri, new { Token = token, ReturnUri = ReturnUri });
                        return new(link);
                    }

                    user = new AppUser()
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    var tempPassword = userManager.GenerateRandomPassword(18);
                    var result = await userManager.CreateAsync(user, tempPassword);

                    if (result.Succeeded)
                    {
                        await emailSender.SendAccountRegisteredOnExternalLoginMessage(new AccountRegisteredOnExternalLoginMessage()
                        {
                            To = email,
                            Subject = $"Account created with {externalLoginInfo!.ProviderDisplayName} sign in",
                            TempPassword = tempPassword,
                            UserName = email,
                            ProviderName = externalLoginInfo!.ProviderDisplayName!
                        });

                        token = tokenHandler.GenerateToken(user);
                        var link = UrlGenerator.ActionLink("/account/confirm-external-login", frontendUri, new { Token = token, ReturnUri = ReturnUri });
                        return new(link);
                    }
                }

                return new(new NullCredentialsException());
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<ExternalProviderDto>>> GetExternalProvidersAsync()
        {
            try
            {
                var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();
                var providers = schemes.Select(p => new ExternalProviderDto() { Name = p.Name });
                return new(providers);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangeEmailResponse>> RequestChangeEmailAsync(ChangeEmailRequest changeEmailRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(changeEmailRequest.CurrentEmail);

                if (user is not null)
                {
                    var validationResult = await emailValidator.ValidateAsync(changeEmailRequest);

                    if (validationResult.IsValid)
                    {
                        var token = await userManager.GenerateChangeEmailTokenAsync(user, changeEmailRequest.NewEmail);
                        var encodedToken = Uri.EscapeDataString(token);
                        var link = UrlGenerator.ActionLink("/account/change-email", frontendUri, new
                        {
                            changeEmailRequest.CurrentEmail,
                            changeEmailRequest.NewEmail,
                            Token = encodedToken
                        });

                        await emailSender.SendChangeEmailMessage(new ChangeEmailMessage()
                        {
                            Link = link,
                            To = changeEmailRequest.CurrentEmail,
                            Subject = "Change email address request",
                            UserName = changeEmailRequest.CurrentEmail,
                            NewEmail = changeEmailRequest.NewEmail,
                        });

                        return new(new ChangeEmailResponse()
                        {
                            Message = "We have sent an email with instructions to your email."
                        });
                    }

                    return new(new FailedValidationException(validationResult.Errors));

                }

                return new(new NotFoundUserByEmailException(changeEmailRequest.CurrentEmail));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ConfirmChangeEmailResponse>> ConfirmChangeEmailAsync(ConfirmChangeEmailRequest confirmChangeEmailRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(confirmChangeEmailRequest.CurrentEmail);

                if (user is not null)
                {
                    var token = Uri.UnescapeDataString(confirmChangeEmailRequest.Token);
                    var result = await userManager.ChangeEmailAsync(user, confirmChangeEmailRequest.NewEmail, token);

                    if (result.Succeeded)
                    {
                        return new(new ConfirmChangeEmailResponse()
                        {
                            Message = "Your email address was successfully changed."
                        });
                    }

                    return new(new NotChangedEmailException());
                }

                return new(new NotFoundUserByEmailException(confirmChangeEmailRequest.CurrentEmail));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangeUserNameResponse>> ChangeUserNameAsync(ChangeUserNameRequest changeUserNameRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(changeUserNameRequest.Email);

                if (user is not null)
                {
                    var validationResult = await userNameValidator.ValidateAsync(changeUserNameRequest);

                    if (validationResult.IsValid)
                    {
                        var result = await userManager.SetUserNameAsync(user, changeUserNameRequest.UserName);

                        if (result.Succeeded)
                        {
                            user = await userManager.FindByEmailAsync(changeUserNameRequest.Email);
                            var token = tokenHandler.GenerateToken(user!);

                            return new(new ChangeUserNameResponse()
                            {
                                Message = "Your user name was successfully changed.",
                                Token = token
                            });
                        }

                        return new(new NotChangedUserNameException());
                    }

                    return new(new FailedValidationException(validationResult.Errors));
                }

                return new(new NotFoundUserByEmailException(changeUserNameRequest.Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public Result<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var newToken = tokenHandler.RefreshToken(refreshTokenRequest.Token);

                return new(new RefreshTokenResponse() { Token = newToken });
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ChangePhoneNumberResponse>> RequestChangePhoneNumberAsync(ChangePhoneNumberRequest changePhoneNumberRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(changePhoneNumberRequest.Email);

                if (user is not null)
                {
                    var validationResult = await phoneValidator.ValidateAsync(changePhoneNumberRequest);

                    if (validationResult.IsValid)
                    {
                        var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, changePhoneNumberRequest.PhoneNumber);
                        var link = UrlGenerator.ActionLink("/account/change-phone-number", frontendUri, new
                        {
                            Token = token,
                            Email = changePhoneNumberRequest.Email,
                            PhoneNumber = changePhoneNumberRequest.PhoneNumber
                        });

                        await emailSender.SendChangePhoneNumberMessage(new ChangePhoneNumberMessage()
                        {
                            Link = link,
                            To = changePhoneNumberRequest.Email,
                            Subject = "Change phone number request",
                            UserName = changePhoneNumberRequest.Email,
                            PhoneNumber = changePhoneNumberRequest.PhoneNumber
                        });

                        return new(new ChangePhoneNumberResponse()
                        {
                            Message = "We have sent you an email with instructions."
                        });
                    }

                    return new(new FailedValidationException(validationResult.Errors));
                }

                return new(new NotFoundUserByEmailException(changePhoneNumberRequest.Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ConfirmChangePhoneNumberResponse>> ConfirmChangePhoneNumberAsync(ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(confirmChangePhoneNumberRequest.Email);

                if (user is not null)
                {
                    var token = Uri.UnescapeDataString(confirmChangePhoneNumberRequest.Token);
                    var result = await userManager.ChangePhoneNumberAsync(user, confirmChangePhoneNumberRequest.PhoneNumber, token);

                    if (result.Succeeded)
                    {
                        return new(new ConfirmChangePhoneNumberResponse() { Message = "Your phone number was successfully changed." });
                    }

                    return new(new NotChangedPhoneNumberException());
                }

                return new(new NotFoundUserByEmailException(confirmChangePhoneNumberRequest.Email));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }
}
