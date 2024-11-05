using eShop.Application.Validation.Products;

namespace eShop.ProductWebApi.Validation;

public class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Request).SetValidator(new CreateProductValidator());
    }
}