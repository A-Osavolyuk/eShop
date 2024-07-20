using eShop.Domain.DTOs.Requests.Cart;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.CartId)
                .IsValidGuid().WithMessage("Invalid card id");

            RuleForEach(x => x.Goods)
                .SetValidator(new GoodValidator()).When(x => x.Goods.Any());

            RuleFor(x => x.GoodsCount)
                .GreaterThan(-1).WithMessage("Goods count must be greater then 0");
        }
    }
}
