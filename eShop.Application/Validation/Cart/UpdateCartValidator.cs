using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.Requests.Cart;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.CartId)
                .IsValidGuid().WithMessage("Invalid card id");

            RuleForEach(x => x.Products)
                .SetValidator(new ProductValidator()).When(x => x.Products.Any());

            RuleFor(x => x.ProductCount)
                .GreaterThan(-1).WithMessage("Goods count must be greater then 0");
        }
    }
}
