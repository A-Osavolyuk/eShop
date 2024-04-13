using eShop.Domain.DTOs.Requests;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must contain at least 2 letters.")
                .MaximumLength(20).WithMessage("Name cannot be longer then 20 letters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must contain at least 2 letters.")
                .MaximumLength(20).WithMessage("Name cannot be longer then 20 letters.");
        }
    }

//    public class UpdateBrandValidator : AbstractValidator<CreateBrandRequest>
//    {
//    }
}
