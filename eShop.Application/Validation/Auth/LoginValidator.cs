using eShop.Domain.Requests.Auth;
using FluentValidation;

namespace eShop.Application.Validation.Auth
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is must.")
                .EmailAddress().WithMessage("Invalid format of email address.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is must.");
        }
    }
}
