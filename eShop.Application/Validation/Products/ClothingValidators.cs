using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using FluentValidation;

namespace eShop.Application.Validation.Products
{
    public class CreateClothingRequestValidator : AbstractValidator<CreateClothingRequest>
    {
        public CreateClothingRequestValidator()
        {
            RuleFor(x => x.ProductType)
                .Equal(ProductType.Clothing).WithMessage("Invalid product type for clothing. Must be of type clothing.");

            RuleFor(x => x.SupplierId)
                .IsValidGuid().WithMessage("Invalid SupplierId.")
                .NotEmpty().WithMessage("SupplierId is required.");

            RuleFor(x => x.BrandId)
                .IsValidGuid().WithMessage("Invalid BrandId.")
                .NotEmpty().WithMessage("BrandId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 letters.")
                .MaximumLength(64).WithMessage("Name must not be longer then 64 letters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(3).WithMessage("Description must contain at least 16 letters.")
                .MaximumLength(128).WithMessage("Description must not be longer then 128 letters.");

            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage("Invalid currency.");

            RuleFor(x => x.Amount)
                .LessThan(100_000m).WithMessage("Price must be less then 100 000.")
                .GreaterThan(0.01m).WithMessage("Price must be greater then 0,01.");

            RuleFor(x => x.Audience)
                .IsInEnum().WithMessage("Invalid audience.");

            RuleForEach(x => x.Sizes)
                .IsInEnum().WithMessage("Invalid size.");

            RuleForEach(x => x.Colors)
                .IsInEnum().WithMessage("Invalid color.");
        }
    }

    public class UpdateClothingRequestValidator : AbstractValidator<UpdateClothingRequest>
    {
        public UpdateClothingRequestValidator()
        {
            RuleFor(x => x.ProductType)
                .Equal(ProductType.Clothing).WithMessage("Invalid product type for clothing. Must be of type clothing.");

            RuleFor(x => x.SupplierId)
                .IsValidGuid().WithMessage("Invalid SupplierId.")
                .NotEmpty().WithMessage("SupplierId is required.");

            RuleFor(x => x.BrandId)
                .IsValidGuid().WithMessage("Invalid BrandId.")
                .NotEmpty().WithMessage("BrandId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 letters.")
                .MaximumLength(64).WithMessage("Name must not be longer then 64 letters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(3).WithMessage("Description must contain at least 16 letters.")
                .MaximumLength(128).WithMessage("Description must not be longer then 128 letters.");

            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage("Invalid currency.");

            RuleFor(x => x.Amount)
                .LessThan(100_000m).WithMessage("Price must be less then 100 000.")
                .GreaterThan(0.01m).WithMessage("Price must be greater then 0,01.");

            RuleFor(x => x.Audience)
                .IsInEnum().WithMessage("Invalid audience.");

            RuleForEach(x => x.Sizes)
                .IsInEnum().WithMessage("Invalid size.");

            RuleFor(x => x.Color)
                .IsInEnum().WithMessage("Invalid color.");
        }
    }
}
