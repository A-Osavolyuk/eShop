using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class SupplierValidator : AbstractValidator<SupplierDto>
    {
        public SupplierValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is must!")
                .MaximumLength(32).WithMessage("Name cannot be longer then 32 characters.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 characters.");

            RuleFor(p => p.ContactEmail)
                .EmailAddress().WithMessage("Invalid email format.")
                .NotEmpty().WithMessage("Contact Email is must!");
        }
    }
}
