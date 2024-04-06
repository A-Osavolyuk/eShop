using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class PhoneNumberValidator : AbstractValidator<ChangePhoneNumberRequest>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+\(\d{2}\)-\d{3}-\d{3}-\d{4}$|^\d{12}$").WithMessage("Wrong phone number format.")
                .NotEmpty().WithMessage("Phone number is must!");
        }
    }
}
