using AutoMapper;
using eShop.AuthWebApi.Data;
using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.Common;
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
        private readonly IMapper mapper;

        public AuthService(
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, 
            IValidator<RegistrationRequestDto> registrationValidator, 
            IMapper mapper)
        {
            this.tokenHandler = tokenHandler;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.registrationValidator = registrationValidator;
            this.mapper = mapper;
        }

        public ValueTask<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<Result<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest)
        {
            var validationResult = await registrationValidator.ValidateAsync(registrationRequest);

            if (registrationRequest is null)
                return new Result<RegistrationResponseDto>(new NullRequestException(type: registrationRequest.GetType()));

            if (!validationResult.IsValid)
                return new Result<RegistrationResponseDto>(new ValidationException("Validation Error(s)", validationResult.Errors));

            var user = mapper.Map<AppUser>(registrationRequest);
            var registrationResult = await userManager.CreateAsync(user, registrationRequest.Password);

            if (registrationResult.Succeeded)
            {
                return new Result<RegistrationResponseDto>(new RegistrationResponseDto($"User with email: {registrationRequest.Email} have been successfully registered."));
            }

            return new Result<RegistrationResponseDto>(new UserAlreadyExistsException(Email: registrationRequest.Email));
        }
    }
}
