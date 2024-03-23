using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
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
