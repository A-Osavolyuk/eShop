using eShop.Domain.DTOs;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class GoodDtoValidator : AbstractValidator<GoodDTO>
    {
        public GoodDtoValidator()
        {
            RuleFor(x => x.Article)
                .InclusiveBetween(100_000_000m, 100_000_000_000m).WithMessage("Invalid article, must be from 9 to 12 characters long");

            RuleFor(x => x.Price)
                .LessThan(100_000m).WithMessage("Price cannot be higher then 100 000").GreaterThan(0.1m).WithMessage("Price cannot be lower then 0.1");

            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("Good must contain at least one image");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater then 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(8).WithMessage("Name must contain at least 8 characters")
                .MaximumLength(200).WithMessage("Name cannot be longer then 200 characters");
        }
    }
}
