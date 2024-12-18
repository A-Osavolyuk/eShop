using eShop.Domain.Requests.AuthApi.Auth;

namespace eShop.Application.Validation.Auth;

public class ChangePhoneNumberValidator : AbstractValidator<ChangePhoneNumberRequest>
{
    public ChangePhoneNumberValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+\(\d{2}\)-\d{3}-\d{3}-\d{4}$|^\d{12}$").WithMessage("Wrong phone number format.")
            .NotEmpty().WithMessage("Phone number is must!");
    }
}