using eShop.Domain.DTOs;
using eShop.Domain.Entities.Cart;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Article).InclusiveBetween(99_999_999, 1_000_000_000_000).WithMessage("Article length must be between 9 and 12 digits long.");
        }
    }
}
