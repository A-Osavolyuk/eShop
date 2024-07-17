using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using FluentValidation;

namespace eShop.Application.Validation.Products
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(8).WithMessage("Name must contain at least 8 characters")
                .MaximumLength(200).WithMessage("Name cannot be longer then 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .MinimumLength(32).WithMessage("Description must contain at least 32 characters")
                .MaximumLength(2000).WithMessage("Description cannot be longer then 2000 characters");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid value")
                .NotEqual(Category.None).WithMessage("Choose your category");

            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage("Invalid value")
                .NotEqual(Currency.None).WithMessage("Choose your currency");

            RuleFor(x => x.Price)
                .LessThan(100_000m).WithMessage("Price cannot be higher then 100 000")
                .GreaterThan(0.1m).WithMessage("Price cannot be lower then 0.1");

            RuleFor(x => x.Images)
                .Must(x => x.Any()).WithMessage("Your product must contain at least one image");

            RuleForEach(x => x.Images)
                .NotEmpty().WithMessage("Image must have a name").When((product) => product.Images.Any());

            RuleFor(x => x.Compound)
                .NotEmpty().WithMessage("Your have to specify compound of your product");

            RuleFor(x => x.Audience)
                .NotEqual(Audience.None).WithMessage("Pick your audience").When((request) =>
                {
                    return request.Category switch
                    {
                        Category.Clothing => true,
                        Category.Shoes => true,
                        Category.None or _ => false,
                    };
                });

            RuleFor(x => x.Color)
                .NotEqual(ProductColor.None).WithMessage("Pick your color").When((request) =>
                {
                    return request.Category switch
                    {
                        Category.Clothing => true,
                        Category.Shoes => true,
                        Category.None or _ => false,
                    };
                });

            RuleFor(x => x.Sizes).NotNull();

            RuleForEach(x => x.Sizes)
                .NotEqual(ProductSize.None).WithMessage("Pick your sizes").When((request) =>
                {
                    return request.Category switch
                    {
                        Category.Clothing => true,
                        Category.Shoes => true,
                        Category.None or _ => false,
                    };
                });
        }
    }
}
