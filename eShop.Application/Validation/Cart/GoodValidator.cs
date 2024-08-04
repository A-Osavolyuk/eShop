using eShop.Domain.DTOs;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class GoodValidator : AbstractValidator<GoodDTO>
    {
        public GoodValidator()
        {
            RuleFor(x => x.Article).InclusiveBetween(99_999_999, 1_000_000_000_000).WithMessage("Article length must be between 9 and 12 digits long.");
            RuleFor(x => x.Amount).GreaterThan(-1).WithMessage("Amount cannot be negative.");
            RuleFor(x => x.Price).InclusiveBetween(0.1m, 10000m).WithMessage("Price must be between 0.1 and 10000.");
            RuleFor(x => x.GoodId).IsValidGuid();
            RuleFor(x => x.Images).NotNull();
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must contain at least 3 letters.")
                .MaximumLength(64).WithMessage("Name must not be longer then 64 letters.");
        }
    }
}
