using eShop.Domain.Requests.AuthApi.Auth;

namespace eShop.Application.Validation.Auth;

public class TwoFactorLoginValidator : AbstractValidator<TwoFactorAuthenticationLoginRequest>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TwoFactorAuthenticationLoginRequest>
            .CreateWithOptions((TwoFactorAuthenticationLoginRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
    
    public TwoFactorLoginValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code cannot be empty.")
            .Length(6).WithMessage("Must contain 6 characters.");
    }
}