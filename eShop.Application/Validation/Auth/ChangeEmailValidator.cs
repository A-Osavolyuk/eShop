using eShop.Domain.DTOs.Requests.Auth;
using FluentValidation;

namespace eShop.Application.Validation.Auth
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
    {
        public ChangeEmailValidator()
        {
            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("New email is must!")
                .EmailAddress().WithMessage("Invalid email address format.");
        }
    }
}
