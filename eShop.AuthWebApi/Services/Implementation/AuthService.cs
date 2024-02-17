using AutoMapper;
using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.Common;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Exceptions;
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
        private readonly IMapper mapper;

        public AuthService(
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IValidator<RegistrationRequestDto> registrationValidator,
            IValidator<LoginRequestDto> loginValidator,
            IMapper mapper)
        {
            this.tokenHandler = tokenHandler;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.registrationValidator = registrationValidator;
            this.loginValidator = loginValidator;
            this.mapper = mapper;
        }

        public async ValueTask<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            var validationResult = await loginValidator.ValidateAsync(loginRequest);

            if (loginRequest is null)
                return new Result<LoginResponseDto>(new NullRequestException(type: loginRequest.GetType()));

            if (!validationResult.IsValid)
                return new Result<LoginResponseDto>(new FailedValidationException("Validation Error(s)",
                    validationResult.Errors.Select(x => x.ErrorMessage)));

            var loginResult = await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

            if (loginResult.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(loginRequest.Email);
                var token = tokenHandler.GenerateToken(user);
                var userDto = new UserDto(user.Email, user.Name, user.Id);

                return new Result<LoginResponseDto>(new LoginResponseDto(userDto, token));
            }

            return new Result<LoginResponseDto>(new InvalidLoginAttemptException());
        }

        public async ValueTask<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest)
        {
            var validationResult = await registrationValidator.ValidateAsync(registrationRequest);

            if (registrationRequest is null)
                return new Result<RegistrationResponseDto>(new NullRequestException(type: registrationRequest.GetType()));

            if (!validationResult.IsValid)
                return new Result<RegistrationResponseDto>(new FailedValidationException("Validation Error(s)",
                    validationResult.Errors.Select(x => x.ErrorMessage)));

            var user = mapper.Map<AppUser>(registrationRequest);
            var registrationResult = await userManager.CreateAsync(user, registrationRequest.Password);

            if (registrationResult.Succeeded)
            {
                return new Result<RegistrationResponseDto>(
                    new RegistrationResponseDto($"User with email: {registrationRequest.Email} have been successfully registered."));
            }

            return new Result<RegistrationResponseDto>(
                new InvalidRegisterAttemptException("Invalid registration attempt.", registrationResult.Errors.Select(x => x.Description)));
        }
    }
}
