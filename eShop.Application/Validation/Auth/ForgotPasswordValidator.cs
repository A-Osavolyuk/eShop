using eShop.Domain.DTOs.Requests.Auth;
using FluentValidation;

namespace eShop.Application.Validation.Auth
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is must.")
                .EmailAddress().WithMessage("Invalid format of email address.");
        }
    }
}
