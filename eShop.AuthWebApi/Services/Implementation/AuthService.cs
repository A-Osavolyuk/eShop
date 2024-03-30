using LanguageExt;
using Microsoft.AspNetCore.Authentication.Google;
using System;

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
        private readonly IMapper mapper = mapper;
        private readonly IEmailSender emailSender = emailSender;
        private readonly IConfiguration configuration = configuration;
        private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

        public async ValueTask<Result<ChangePasswordResponse>> ChangePasswordAsync(string UserId, ChangePasswordRequest changePasswordRequest)
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
                                return new(new ChangePasswordResponse() { Message = "Password has been successfully changed." });
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

        public async ValueTask<Result<ChangePersonalDataResponse>> ChangePersonalDataAsync(string Id, ChangePersonalDataRequest changePersonalDataRequest)
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
                            return new(new ChangePersonalDataResponse()
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

        public async ValueTask<Result<Unit>> ConfirmEmailAsync(string Email, ConfirmEmailRequest confirmEmailRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var token = new StringBuilder(confirmEmailRequest.Token).Replace(" ", "+").ToString();
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
                        var token = new StringBuilder(confirmPasswordResetRequest.ResetToken).Replace(" ", "+").ToString();
                        var resetResult = await userManager.ResetPasswordAsync(user, token, confirmPasswordResetRequest.NewPassword);

                        if (resetResult.Succeeded)
                        {
                            return new(new ConfirmPasswordResetResponse() { Message = "Your password has been successfully reset." });
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

        public async ValueTask<Result<PersonalData>> GetPersonalDataAsync(string Id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);

                if (user is null)
                    return new(new NotFoundUserByIdException(Id));

                return new(new PersonalData()
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

        public async ValueTask<Result<LoginResponse>> LoginAsync(LoginRequest loginRequest)
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
                    if (user.EmailConfirmed)
                    {
                        var isValidPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                        if (isValidPassword)
                        {
                            var userDto = new UserDto(user.Email!, user.Email!, user.Id);

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

                    var link = UrlGenerator.ActionLink("/account/confirm-email", frontendUri,
                        new { Email = registrationRequest.Email, Token = emailConfirmationToken });

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

                return new(new InvalidRegisterAttemptException("Invalid registration attempt.",
                    registrationResult.Errors.Select(x => x.Description)));
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
                    var link = UrlGenerator.ActionLink("/account/confirm-password-reset", frontendUri, new { Email = UserEmail, Token = token });

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

        public async ValueTask<Result<LoginResponse>> LoginWithTwoFactorAuthenticationCodeAsync(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(Email);

                if (user is not null)
                {
                    var result = await userManager.VerifyTwoFactorTokenAsync(user, "Email", twoFactorAuthenticationLoginRequest.Code);

                    if (result)
                    {
                        var userDto = new UserDto(user.Email!, user.Email!, user.Id);
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

                var isValidProvider = providers.Select(p => p.Name == provider).FirstOrDefault();

                if (isValidProvider)
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

                    var tempPassword = new StringBuilder("A").Append(Guid.NewGuid()).ToString();
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

        public async ValueTask<Result<IEnumerable<ExternalProviderDto>>> GetExternalProviders()
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
    }
}
