using eShop.Domain.DTOs.Requests.Cart;
using FluentValidation;

namespace eShop.Application.Validation.Cart
{
    public class CreateCartValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId).IsValidGuid().WithMessage("Invalid user id");
        }
    }
}
