using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
    {
        public ChangeEmailValidator()
        {
            RuleFor(x => x.NewEmail).EmailAddress().WithMessage("Invalid email address format.");
        }
    }
}
