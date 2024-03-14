using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class SubcategoryValidator : AbstractValidator<CreateUpdateSubcategoryRequestDto>
    {
        public SubcategoryValidator()
        {
            RuleFor(p => p.SubcategoryName)
                .NotEmpty().WithMessage("Name is must!")
                .MaximumLength(32).WithMessage("Name cannot be longer then 32 characters.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 characters.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("CategoryId is must!")
                .IsValidGuid().WithMessage("Invalid Guid format. SubcategoryId length must be 36 characters.");
        }
    }
}
