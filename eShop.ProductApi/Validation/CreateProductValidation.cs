using eShop.Application.Validation.Products;
using eShop.ProductApi.Commands.Products;

namespace eShop.ProductApi.Validation;

internal class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Request).SetValidator(new CreateProductValidator());
    }
}