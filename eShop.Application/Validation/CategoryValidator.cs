using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class CategoryValidator : AbstractValidator<CreateUpdateCategoryRequestDto>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.CategoryName)
                .NotEmpty().WithMessage("Name is must!")
                .MaximumLength(32).WithMessage("Name cannot be longer then 32 characters.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 characters.");
        }
    }
}
