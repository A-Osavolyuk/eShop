using eShop.Domain.DTOs.Responses;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is must!")
                .MaximumLength(32).WithMessage("Name cannot be longer then 32 characters.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 characters.");

            RuleFor(p => p.Price)
                .LessThan(10000).WithMessage("Price cannot higher then 10000.")
                .GreaterThan(0.1m).WithMessage("Price cannot be less than 0.1");

            RuleFor(p => p.SupplierId)
                .NotEmpty().WithMessage("SuppliedId is must!")
                .IsValidGuid().WithMessage("Invalid Guid format. SuppliedId length must be 36 characters.");

            RuleFor(p => p.SubcategoryId)
                .NotEmpty().WithMessage("CategoryId is must!")
                .IsValidGuid().WithMessage("Invalid Guid format. CategoryId length must be 36 characters.");

            RuleFor(p => p.ProductDescription.ShortDescription)
                .NotEmpty().WithMessage("Short Description is must!")
                .MaximumLength(128).WithMessage("Short Description cannot be longer then 128 characters.")
                .MinimumLength(16).WithMessage("Short Description must contain at least 16 characters.");

            RuleFor(p => p.ProductDescription.LongDescription)
                .NotEmpty().WithMessage("Long Description is must!")
                .MaximumLength(512).WithMessage("Long Description cannot be longer then 512 characters.")
                .MinimumLength(16).WithMessage("Long Description must contain at least 16 characters.");

        }
    }
}
