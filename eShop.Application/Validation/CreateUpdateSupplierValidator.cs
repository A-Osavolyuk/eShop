using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
    {
        public CreateSupplierValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is must!")
                .MinimumLength(2).WithMessage("Name must contain at least 2 letters.")
                .MaximumLength(20).WithMessage("Name cannot be longer then 20 letters.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Contact email is must!")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+\(\d{2}\)-\d{3}-\d{3}-\d{4}$|^\d{12}$").WithMessage("Wrong phone number format.")
                .NotEmpty().WithMessage("Contact phone is must!");
        }
    }

    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
    {
        public UpdateSupplierValidator()
        {
            RuleFor(x => x.Id)
                .IsValidGuid();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is must!")
                .MinimumLength(2).WithMessage("Name must contain at least 2 letters.")
                .MaximumLength(20).WithMessage("Name cannot be longer then 20 letters.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Contact email is must!")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+\(\d{2}\)-\d{3}-\d{3}-\d{4}$|^\d{12}$").WithMessage("Wrong phone number format.")
                .NotEmpty().WithMessage("Contact phone is must!");
        }
    }
}
