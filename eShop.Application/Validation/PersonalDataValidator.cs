using eShop.Domain.DTOs.Requests;
using FluentValidation;

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

            RuleFor(x => x.MiddleName)
                .NotEmpty().WithMessage("Middle Name is must!")
                .MinimumLength(3).WithMessage("Middle Name must be longer then 3 letters.")
                .MaximumLength(32).WithMessage("Middle Name must be less then 3 letters.");

            RuleFor(x => x.PhoneNumber)
                .Matches("^[0-9\\-\\+]{9,15}$").WithMessage("Wrong phone number format.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is must!")
                .MinimumLength(3).WithMessage("Gender must length be longer then 3 letters.")
                .MaximumLength(32).WithMessage("Gender must length be less then 3 letters.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("You cannot choose today`s date.");
        }
    }
}
