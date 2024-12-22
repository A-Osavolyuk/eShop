using eShop.Domain.Requests.AuthApi.Auth;

namespace eShop.Application.Validation.Auth;

public class VerifyEmailValidator : AbstractValidator<VerifyEmailRequest>
{
    public VerifyEmailValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(6).WithMessage("Code length must be 6 characters.")
            .Matches(@"^\d+$").WithMessage("Code must be numeric.");
    }
}