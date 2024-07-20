using eShop.Domain.DTOs.Requests.Cart;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class AddGoodToCartValidator : AbstractValidator<AddGoodToCartRequest>
    {
        public AddGoodToCartValidator()
        {
            RuleFor(x => x.CartId).IsValidGuid().WithMessage("Invalid card id");
            RuleFor(x => x.Good).NotNull().WithMessage("Good must not be null");
            RuleFor(x => x.Good).SetValidator(new GoodValidator());
        }
    }
}
