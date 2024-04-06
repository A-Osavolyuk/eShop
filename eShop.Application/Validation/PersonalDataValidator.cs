using eShop.Domain.DTOs.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace eShop.Application.Validation
{
    public class PersonalDataValidator : AbstractValidator<ChangePersonalDataRequest>
    {
        public PersonalDataValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is must!")
                .MinimumLength(3).WithMessage("First Name length must be longer then 3 letters.")
                .MaximumLength(32).WithMessage("First Name length must be less then 3 letters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("First Name is must!")
                .MinimumLength(3).WithMessage("Last Name length must be longer then 3 letters.")
                .MaximumLength(32).WithMessage("Last Name length must be less then 3 letters.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+\(\d{2}\)-\d{3}-\d{3}-\d{4}$|^\d{12}$").WithMessage("Wrong phone number format.")
                .NotEmpty().WithMessage("Phone number is must!");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is must!")
                .MinimumLength(3).WithMessage("Gender must length be longer then 3 letters.")
                .MaximumLength(32).WithMessage("Gender must length be less then 3 letters.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("You cannot choose today`s date.");
        }
    }
}
