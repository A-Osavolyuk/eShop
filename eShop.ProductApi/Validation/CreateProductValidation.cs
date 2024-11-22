namespace eShop.ProductApi.Validation;

internal sealed class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Request).SetValidator(new CreateProductValidator());
    }
}